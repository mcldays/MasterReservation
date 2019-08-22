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

        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }

        public string Information { get; set; }
    }
}