using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        Email = model.Email,
                        Phone = model.PhoneNumber,
                        City = model.City,
                        Offers = model.Offers,
                        Experience = model.Expirience,
                        Awards = model.Awards,
                        Password = model.Password



                    });

                    DbUse.SaveChanges();

                }






            }
            catch (Exception e)
            {
                return true;


            }



            return true;
        }



    }
}