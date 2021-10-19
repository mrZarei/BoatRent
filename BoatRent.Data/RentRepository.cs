using BoatRent.Core.Domain;
using BoatRent.Core.Interfaces;
using BoatRent.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoatRent.Data
{
    public class RentRepository : IBoatRentalRepository
    {
        private RentDbContext _dbContext;

        public RentRepository(RentDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> BoatExists(string boatNumber)
        {
            return await _dbContext.Boats.AnyAsync(b => b.BoatNumber == boatNumber);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<RentalDto> GetLastOpenRentFor(string boatNumber)
        {
            var item = await _dbContext.RentBoat
                .Include("Boat")
                .Where(r => r.Boat.BoatNumber == boatNumber && !r.IsReturned)
                .OrderBy( r => r.StartDate).LastOrDefaultAsync();
            if (item == null) return null;

            // We can use also autmapper for doing mapping like this, but I decided to do that explicity in order to keep it simple.
            var result = new RentalDto
            {
                BoatNumber = boatNumber,
                IsReturned = false,
                BoatType = (Boat.BoatType)Enum.Parse(typeof(Boat.BoatType), item.Boat.BoatType),
                BookingNumber = item.BookingNumber,
                CustomerNumber = item.CustomerNumber,
                StartDate = item.StartDate,
            };
            return result;
        }

        public async Task<bool> IsBoatAvailable(string boatNumber)
        {
            var isInRent = await _dbContext.RentBoat.AnyAsync(r => r.Boat.BoatNumber == boatNumber && !r.IsReturned);
            return !isInRent;
        }

        public async Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate)
        {
            try
            {
                var boat = await GetBoat(boatNumber);
                if (boat == null)
                {
                    // Register first the boat
                    boat = new Models.Boat { BoatNumber = boatNumber, BoatType = type.ToString(), };
                }
                var rentEntity = new Models.RentBoat
                {
                    BookingNumber = bookingNumber,
                    CustomerNumber = customerNumber,
                    StartDate = startDate,
                    IsReturned = false,
                    Boat = boat,
                };
                await _dbContext.RentBoat.AddAsync(rentEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<RentalDto> ReturnBoat(string bookingNumber, DateTime endDate)
        {
            var rentEntity = await _dbContext.RentBoat.FindAsync(bookingNumber);
            if (rentEntity != null)
            {
                rentEntity.IsReturned = true;
                rentEntity.EndDate = endDate;
                await _dbContext.SaveChangesAsync();
                return new RentalDto
                {
                    BookingNumber = rentEntity.BookingNumber,
                    CustomerNumber = rentEntity.CustomerNumber,
                    StartDate = rentEntity.StartDate,
                    IsReturned = rentEntity.IsReturned,
                    BoatNumber = rentEntity.Boat.BoatNumber,
                    BoatType = (Boat.BoatType) Enum.Parse(typeof(Boat.BoatType),rentEntity.Boat.BoatType),
                    EndDate = rentEntity.EndDate
                };
            }
            return null;
        }

        private async Task<Models.Boat> GetBoat(string boatNumber)
        {
            return await _dbContext.Boats.FindAsync(boatNumber);
        }
    }
}
