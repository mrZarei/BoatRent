using System;
using System.ComponentModel.DataAnnotations;

namespace BoatRent.Web.Models
{
    public class ReturnBoatModel
    {
        [Display(Name = "Boat number")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string BoatNumber { get; set; }
        [Display(Name = "Return Date")]
        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime EndDate {  get; set; }
    }
}
