using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ResidentId { get; set; }
        public int PlaceId { get; set; }
        public int SalonId { get; set; }
        public DateTime Date { get; set; }
        public string Times { get; set; }
        public string Info { get; set; }
        public double Sum { get; set; }
        public bool Confirmed { get; set; }
    }
}