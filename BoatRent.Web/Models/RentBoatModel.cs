using System;
using System.ComponentModel.DataAnnotations;
using static BoatRent.Core.Domain.Boat;

namespace BoatRent.Web.Models
{
    public class RentBoatModel
    {
        [Display(Name ="Booking number")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string BookingNumber { get; set; }
        [Display(Name = "Boat number")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string BoatNumber { get; set; }
        [Display(Name = "Boat type")]
        [Required(ErrorMessage = "This field is mandatory")]
        public BoatType BoatType { get; set; }
        [Display(Name = "Customer Number")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string CustomerNumber { get; set; }
        [Display(Name = "Start date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime StartDate { get; set; }
    }
}
