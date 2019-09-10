using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterReservation.Models;

namespace MasterReservation.Utilities
{
    public static class GetData
    {
        public static ResidentModel GetDataResident(string Email)
        {
            using (UserContext DbUse = new UserContext())
            {
                ResidentModel user = DbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                return user;

            }
        }

        public static SalonModel GetDataSalonAdmin(string Email)
        {
            using (UserContext DbUse = new UserContext())
            {
                SalonModel user = DbUse.SalonModels.FirstOrDefault(t => t.Email == Email);
                return user;

            }
        }
    }
}