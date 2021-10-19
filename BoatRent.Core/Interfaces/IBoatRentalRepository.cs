using BoatRent.Core.Domain;
using BoatRent.Core.Models;
using System;
using System.Threading.Tasks;

namespace BoatRent.Core.Interfaces
{
    public interface IBoatRentalRepository : IDisposable
    {
        Task Register(string boatNumber, Boat.BoatType type, string bookingNumber, string customerNumber, DateTime startDate);
        Task<bool> IsBoatAvailable(string boatNumber);
        Task<bool> BoatExists(string boatNumber);
        Task<RentalDto> GetLastOpenRentFor(string boatNumber);
        Task<RentalDto> ReturnBoat(string bookingNumber, DateTime endDate);
    }
}
