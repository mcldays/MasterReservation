using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class DateModel
    {
        [Key]
        public int Id { get; set; }

        public string Date { get; set; }

        public string TimeFirst { get; set; } 

        public string TimeSecond { get; set; }

        public bool FullTime { get; set; }

        public string Offers { get; set; }

        public string City { get; set; }

        public bool PrivateSpace { get; set; }
        
        public bool OpenSpace { get; set; }

        public string AddedOffers { get; set; }
        




    }
}