using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class FavoriteModel
    {
        [Key]
        public int Id { get; set; }

        public string SalonTitle { get; set; }
        public string SalonAdress { get; set; }
        public int PlaceId { get; set; }
        public double Rate1h { get; set; }
        public double Rateday { get; set; }
        public double RateHalfMounth { get; set; }
        public double RateMounth { get; set; }
    }
}