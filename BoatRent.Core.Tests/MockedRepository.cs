using BoatRent.Core.Domain;
using BoatRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatRent.Core.Interfaces
{
    internal class MockedRepository : IBoatRentalRepository
    {
        List<RentalDto> dbset;
        public MockedRepository()
        {
            dbset = new List<RentalDto>();
        }
        public Task<bool> BoatExists(string boatNumber)
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            dbset.Clear();
        }

        public Task<RentalDto> GetLastOpenRentFor(string boatNumber)
        {
            return Task.FromResult(dbset.FindLast(r => r.BoatNumber == boatNumber && !r.IsReturned));
        }

        public Task<bool> IsBoatAvailable(string boatNumber)
        {
            return Task.FromResult(!dbset.Any(r => r.BoatNumber == boatNumber && !r.IsReturned));
        }

        public Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate)
        {
            dbset.Add(new RentalDto
            {
                BoatNumber = boatNumber,
                BoatType = type,
                BookingNumber = bookingNumber,
                StartDate = startDate,
                CustomerNumber = customerNumber,
                IsReturned = false
            });

            return Task.CompletedTask;
        }

        public Task<RentalDto> ReturnBoat(string bookingNumber, DateTime endDate)
        {
            var rentalBoat = dbset.Find(r => r.BookingNumber == bookingNumber);
            rentalBoat.IsReturned = true;
            rentalBoat.EndDate = endDate;
            return Task.FromResult(rentalBoat);
        }
    }
}
