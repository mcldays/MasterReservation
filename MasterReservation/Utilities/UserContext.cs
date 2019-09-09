using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MasterReservation.Models;

namespace MasterReservation.Utilities
{
    class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection")
        {

        }
        public DbSet<ResidentModel> ResidentModels { get; set; }
        public DbSet<SalonModel> SalonModels { get; set; }
        public DbSet<WorkingPlaceModel> WorkingPlaceModels { get; set; }
        public DbSet<TimeSlotModel> TimeSlotModels { get; set; }
        public DbSet<BookingModel> BookingModels { get; set; }
        
        //public DbSet<DateModel> DateModels { get; set; }
    }
}