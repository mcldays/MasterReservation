using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using MasterReservation.Models;


namespace MasterReservation.Utilities
{
    public static class SendDbUtility
    {
        public static bool SendMaster (RegisterMasterModel model)
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


        public static bool CompareAut(LoginMaster model)
        {
            
            
                using (UserContext dbUse = new UserContext())
                {

                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == model.Email && t.Password == model.Password);

                    if (user == null) return false;
                    return true;
               

                }


            
        }


        public static bool SendSalon(RegisterSalonModel model)
        {

            try
            {
                using (UserContext DbUse = new UserContext())
                {



                    DbUse.SalonModels.Add(new SalonModel()
                    {
                      ContactPerson = model.Name,
                      Phone = model.Phone,
                      City = model.City,
                      Email = model.Email,
                      Information = model.Information



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
