using System;
using System.ComponentModel.DataAnnotations;

namespace BoatRent.Data.Models
{
    public class RentBoat
    {
        [Key]
        public string BookingNumber { get; set; }
        public string CustomerNumber { get; set; }
        public Boat Boat { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
