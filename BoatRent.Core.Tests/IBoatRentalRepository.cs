﻿using BoatRent.Core.Domain;
using System;
using System.Threading.Tasks;

namespace BoatRent.Core.Tests
{
    public interface IBoatRentalRepository : IDisposable
    {
        Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate);
        Task<bool> IsBoatAvailable(string boatNumber);
        Task<bool> BoatExists(string boatNumber);
        Task<RentalViewModel> GetLastOpenRentFor(string boatNumber);
        Task<RentalViewModel> ReturnBoat(string bookingNumber, DateTime endDate);
    }
}
