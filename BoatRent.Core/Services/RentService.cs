using BoatRent.Core.Domain;
using BoatRent.Core.Interfaces;
using BoatRent.Core.Models;
using System;
using System.Threading.Tasks;
using static BoatRent.Core.Domain.Boat;

namespace BoatRent.Core.Services
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

        public async Task<RentBoatResult> RentBoat(string boatNumber, BoatType boatType, string bookingNumber, string customerNumber, DateTime startDate)
        {
            try
            {
                if (await _repository.IsBoatAvailable(boatNumber))
                {
                    await _repository.Register(boatNumber, boatType, bookingNumber, customerNumber, startDate);
                    return new RentBoatResult { IsSucceed = true };
                }
                else
                {
                    throw new Exception($"Boat {boatNumber} is not availble");
                }
            }
            catch (Exception ex)
            {
                // Here we should log the exception
                return new RentBoatResult
                {
                    IsSucceed = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReceiptDto> ReturnBoat(string boatNumber, DateTime endDate)
        {
            try
            {
                RentalDto rentedObject = await _repository.GetLastOpenRentFor(boatNumber);
                if (rentedObject == null)
                {
                    throw new Exception($"Boat {boatNumber} has no open booking.");
                }

                RentalDto rentalViewModel = await _repository.ReturnBoat(rentedObject.BookingNumber, endDate);
                RentalDto booking = rentalViewModel;
                Boat boat = Build(booking.BoatType, booking.BoatNumber);
                if (boat == null)
                {
                    throw new Exception($"There is no implementation for boat type {booking.BoatType}");
                }
                return new ReceiptDto
                {
                    BoatNumber = booking.BoatNumber,
                    EndDate = endDate,
                    BoatType = booking.BoatType,
                    BookingNumber = booking.BookingNumber,
                    CustomerNumber = booking.CustomerNumber,
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
