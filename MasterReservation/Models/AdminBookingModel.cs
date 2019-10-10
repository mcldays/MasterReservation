using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class AdminBookingModel
    {
        public int JobPlaceId { get; set; }

        public List<TimeSlotModel> TimeSlotAdmin { get; set; }

        public List<WorkingPlaceModel> bookingTimeList { get; set; }

        public List<string> AnyTimeInformList { get; set; }


    }
}