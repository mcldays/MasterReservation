using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using MasterReservation.Models;

namespace MasterReservation.Utilities
{
    public static class ResidentSend
    {
        public static bool Send (RegisterMasterModel model)
        {
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    DbUse.ResidentModels.Add(new ResidentModel()
                    {



                    });



                }






            }
            catch (Exception e)
            {
               
            }



            return true;
        }



    }
}