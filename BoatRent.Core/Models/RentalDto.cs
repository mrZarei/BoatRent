using System;
using static BoatRent.Core.Domain.Boat;

namespace BoatRent.Core.Models
{
    public class RentalDto
    {
        public string BookingNumber { get; set; }
        public string BoatNumber { get; set; }
        public BoatType BoatType { get; set; }
        public string CustomerNumber {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReturned {  get; set; }
    }
}