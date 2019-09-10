using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class SalonModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SalonTitle { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        [RegularExpression(@"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}(\d{2}){2}$")]
        public string Phone { get; set; }
        public string City { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Adress { get; set; }
        public string OperatingMode { get; set; }
        public bool ReservationType { get; set; }
    }
}