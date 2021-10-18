using BoatRent.Core.Domain;
using BoatRent.Core.Interfaces;
using BoatRent.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRent.Data
{
    internal class RentRepository : IBoatRentalRepository
    {
        private RentDbContext _dbContext;

        public RentRepository(RentDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> BoatExists(string boatNumber)
        {
            return await _dbContext.Boats.AnyAsync( b => b.BoatNumber == boatNumber );
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<RentalViewModel> GetLastOpenRentFor(string boatNumber)
        {
            var item = await _dbContext.RentBoat.Where( r => r.Boat.BoatNumber  == boatNumber && !r.IsReturned).LastOrDefaultAsync();
            if ( item == null) return null;
            
            // We can use also autmapper for doing mapping like this, but I decided to do that explicity in order to keep it simple.
            var result = new RentalViewModel
            {
                BoatNumber = item.Boat.BoatNumber,
                IsReturned = false,
                BoatType = (Boat.BoatType)Enum.Parse(typeof(Boat.BoatType), item.Boat.BoatType),
                BookingNumber = item.BookingNumber,
                StartDate = item.StartDate,
            };
            return result;
        }

        public async Task<bool> IsBoatAvailable(string boatNumber)
        {
            var isInRent = await _dbContext.RentBoat.AnyAsync( r => r.Boat.BoatNumber == boatNumber && !r.IsReturned);
            return !isInRent;
        }

        public async Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate)
        {
            var boat = await GetBoat(boatNumber);
            if ( boat == null )
            {
                // Register first the boat
                await _dbContext.Boats.AddAsync(new Models.Boat
                {
                    BoatNumber = boatNumber,
                    BoatType = type.ToString(),
                });
                boat = new Models.Boat { BoatNumber = boatNumber, BoatType = type.ToString(), };
            }
            await _dbContext.RentBoat.AddAsync(new Models.RentBoat
            {
                BookingNumber= boatNumber,
                CustomerNumber = customerNumber,
                StartDate = startDate,
                IsReturned = false,
                Boat = boat,
            });
            await _dbContext.SaveChangesAsync();
        }

        public Task<RentalViewModel> ReturnBoat(string bookingNumber, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        private async Task<Models.Boat> GetBoat(string boatNumber)
        {
            return await _dbContext.Boats.FindAsync(boatNumber);
        }
    }
}
