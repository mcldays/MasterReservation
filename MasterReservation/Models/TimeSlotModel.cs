using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class TimeSlotModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlaceId { get; set; }
        [Required]
        public int SalonId { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public bool Booked { get; set; }
        public int ResidentId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int BookingId { get; set; }
    }
}