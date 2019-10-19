using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                try
                {
                    ResidentModel user = DbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                    return user;
                }
                catch (Exception e)
                {
                    return null;
                }
                
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

        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

    }


    
}