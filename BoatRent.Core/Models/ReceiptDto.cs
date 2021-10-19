using System;
using static BoatRent.Core.Domain.Boat;

namespace BoatRent.Core.Models
{
    public class ReceiptDto
    {
        public string BookingNumber { get; set; }
        public string BoatNumber { get; set; }
        public BoatType BoatType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
