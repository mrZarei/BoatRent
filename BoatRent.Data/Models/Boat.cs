using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BoatRent.Data.Models
{
    public class Boat
    {
        [Key]
        public string BoatNumber { get; set; }
        public string BoatType {  get; set; }
    }
}
