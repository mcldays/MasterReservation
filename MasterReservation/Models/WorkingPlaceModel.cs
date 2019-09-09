using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class WorkingPlaceModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Offers { get; set; }
        [Required]
        public double Rate1h { get; set; }
        [Required]
        public double Rate3h { get; set; }
        [Required]
        public double RateDay { get; set; }
        [Required]
        public string PlaceType { get; set; }
        public string AdditionalConditions { get; set; }
        public string Description { get; set; }

        public int SalonId { get; set; }

    }
}