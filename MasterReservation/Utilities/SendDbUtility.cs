using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        //Метод занесения данных мастера в бд
        public static bool SendMaster(RegisterMasterModel model)
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
                        Password = model.Password,
                        IsAdmin = false
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

        // Метод занесения данных салона в бд
        public static bool SendSalon(SalonModel model)
        {
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    DbUse.SalonModels.Add(model);
                    DbUse.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return true;
            }
            return true;
        }

        // Метод изменения данных мастера в бд
        public static bool ChangeResident(RegisterMasterModel model)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Id == model.id);

                if (user == null) return false;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Awards = model.Awards;
                user.Offers = model.Offers;
                user.Phone = model.PhoneNumber;
                user.Patronymic = model.Patronymic;
                user.Experience = model.Expirience;
                dbUse.ResidentModels.AddOrUpdate(user);
                dbUse.SaveChanges();
                return true;
            }
        }

        // Метод проверки введенного логина и пароля
        public static bool ComparePassword(string Password, string Email)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user =
                    dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email && t.Password == Password);
                
                if (user == null) return false;
                return true;
            }
        }

        // Метод изменения пароля
        public static bool ChangePassword(string Email, string newPass)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                if (user == null) return false;
                user.Password = newPass;
                dbUse.ResidentModels.AddOrUpdate(user);
                dbUse.SaveChanges();
                return true;
            }
        }

        // Метод проверки существования емэйла в бд
        public static bool CheckEmail(string Email)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                if (user == null) return false;
                return true;
            }
        }

        // Метод проверки существования номера телефона в бд
        public static bool CheckPhone(string Phone)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Phone == Phone);
                if (user == null) return false;
                return true;
            }
        }

        public static bool IsAdmin(string email)
        {
            using (UserContext dbUse = new UserContext())
            {
                ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == email && t.IsAdmin);
                if (user == null) return false;
                return true;
            }
        }


        //public static bool SendDate(DateModel model)
        //{
        //    try
        //    {
        //        using (UserContext DbUse = new UserContext())
        //        {
        //            DbUse.DateModels.AddOrUpdate(model);
        //             DbUse.SaveChanges();
        //            return true;
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        return true;
        //    }
        //}

    }


}
