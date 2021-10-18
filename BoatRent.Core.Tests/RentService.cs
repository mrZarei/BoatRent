using BoatRent.Core.Domain;
using System;
using static BoatRent.Core.Domain.Boat;

namespace BoatRent.Core.Tests
{
    public class RentService
    {
        private IBoatRentalRepository _repository;
        private decimal _hourlyFee;
        private decimal _basicFee;

        public RentService(IBoatRentalRepository repository, decimal hourlyFee, decimal basicFee)
        {
            _repository = repository;
            _hourlyFee = hourlyFee;
            _basicFee = basicFee;
        }

        public RentBoatResulatViewModel RentBoat(string boatNumber, BoatType boatType, string bookingNumber, string customerNumber, DateTime startDate)
        {
           throw new NotImplementedException();
        }

        public ReceiptViewModel ReturnBoat(string boatNumber, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
