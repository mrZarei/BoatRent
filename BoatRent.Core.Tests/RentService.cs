using BoatRent.Core.Domain;
using System;
using System.Threading.Tasks;
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

        public async Task<RentBoatResulatViewModel> RentBoat(string boatNumber, BoatType boatType, string bookingNumber, string customerNumber, DateTime startDate)
        {
            try
            {
                if (await _repository.BoatExists(boatNumber))
                {
                    if (await _repository.IsBoatAvailable(boatNumber))
                    {
                        await _repository.Register(boatNumber, boatType, bookingNumber, customerNumber, startDate);
                        return new RentBoatResulatViewModel { IsSucceed = true };
                    }
                    else
                    {
                        throw new Exception($"Boat {boatNumber} is not availble");
                    }
                }
                else
                {
                    throw new Exception($"Boat {boatNumber} could not be found.");
                }
            }
            catch (Exception ex)
            {
                // Here we should log the exception
                return new RentBoatResulatViewModel
                {
                    IsSucceed = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReceiptViewModel> ReturnBoat(string boatNumber, DateTime endDate)
        {
            try
            {
                RentalViewModel rentedObject = await _repository.GetLastOpenRentFor(boatNumber);
                if (rentedObject == null)
                {
                    throw new Exception($"Boat {boatNumber} has no open booking.");
                }

                RentalViewModel rentalViewModel = await _repository.ReturnBoat(rentedObject.BookingNumber, endDate);
                RentalViewModel booking = rentalViewModel;
                Boat boat = Build(booking.BoatType, booking.BoatNumber);
                if (boat == null)
                {
                    throw new Exception($"There is no implementation for boat type {booking.BoatType}");
                }
                return new ReceiptViewModel
                {
                    BoatNumber = booking.BoatNumber,
                    EndDate = endDate,
                    BoatType = booking.BoatType,
                    BookingNumber = booking.BookingNumber,
                    StartDate = booking.StartDate,
                    Price = boat.CalculatePrice(booking.StartDate, booking.EndDate, _hourlyFee, _basicFee)
                };
            }
            catch (Exception ex)
            {
                // Log exception 
                return null;
            }
        }
    }
}
