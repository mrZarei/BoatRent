using BoatRent.Core.Domain;
using BoatRent.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoatRent.Core.Interfaces
{
    internal class MockedRepository : IBoatRentalRepository
    {
        List<RentalViewModel> dbset;
        public MockedRepository()
        {
            dbset = new List<RentalViewModel>();
        }
        public Task<bool> BoatExists(string boatNumber)
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            dbset.Clear();
        }

        public Task<RentalViewModel> GetLastOpenRentFor(string boatNumber)
        {
            return Task.FromResult(dbset.FindLast(r => r.BoatNumber == boatNumber && !r.IsReturned));
        }

        public Task<bool> IsBoatAvailable(string boatNumber)
        {
            return Task.FromResult(!dbset.Any(r => r.BoatNumber == boatNumber && !r.IsReturned));
        }

        public Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate)
        {
            dbset.Add(new RentalViewModel
            {
                BoatNumber = boatNumber,
                BoatType = type,
                BookingNumber = bookingNumber,
                StartDate = startDate,
                IsReturned = false
            });

            return Task.CompletedTask;
        }

        public Task<RentalViewModel> ReturnBoat(string bookingNumber, DateTime endDate)
        {
            var rentalBoat = dbset.Find(r => r.BookingNumber == bookingNumber);
            rentalBoat.IsReturned = true;
            rentalBoat.EndDate = endDate;
            return Task.FromResult(rentalBoat);
        }
    }
}
