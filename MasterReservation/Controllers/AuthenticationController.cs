using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterMaster(RegisterMasterModel model)
        {
            //FormsAuthentication.SetAuthCookie("Васильев Вася", false);
            if (SendDbUtility.CheckEmail(model.Email))
            {
                TempData["WrongMessage"] = "Такой email уже зарегистрирован!";
                return RedirectToAction("MainPage", "TimerClub");
            }

            if (SendDbUtility.CheckPhone(model.PhoneNumber))
            {
                TempData["WrongMessage"] = "Такой номер телефона уже зарегистрирован!";
                return RedirectToAction("MainPage", "TimerClub");
            }
            Utilities.SendDbUtility.SendMaster(model);
            return RedirectToAction("MainPage", "TimerClub");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginMaster(LoginMaster model)
        {
            if (SendDbUtility.CompareAut(model) == true)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                var resident = GetData.GetDataResident(model.Email);
                Response.Cookies["ResidentName"].Value = Server.UrlEncode(resident.Name);
                Response.Cookies["ResidentSurname"].Value = Server.UrlEncode(resident.Surname);

                return RedirectToAction("FindWorkPlaces", "TimerClub");
            }
            return RedirectToAction("MainPage", "TimerClub");
        }

        [AllowAnonymous]
        public ActionResult RegisterSalon(RegisterSalonModel model)
        {
            Utilities.SendDbUtility.SendSalon(model);

            return RedirectToAction("MainPage", "TimerClub");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("MainPage", "TimerClub");
        }

        public ActionResult UpdateResident(RegisterMasterModel model)
        {
            if (model.Offers == null)
            {
                TempData["WrongMessage"] = "Выберите предоставляемые услуги!";
                return RedirectToAction("PersonalData", "TimerClub");
            }
            if (SendDbUtility.ComparePassword(model.Password, User.Identity.Name))
            {
                Utilities.SendDbUtility.ChangeResident(model);
                TempData["SuccessMessage"] = "Данные изменены!";
                return RedirectToAction("PersonalData", "TimerClub");
            }
            else
            {
                TempData["WrongMessage"] = "Пароль введен не верно!";
                return RedirectToAction("PersonalData", "TimerClub");
            }
        }

        
        public JsonResult CheckPassword(string Password)
        {
            var result = SendDbUtility.ComparePassword(Password, User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ChangePasswordModel ChangePassword(ChangePasswordModel model)
        {
            return null;
        }


        [HttpPost]
        public ActionResult PasswordChange(ChangePasswordModel model)
        {

            if (!SendDbUtility.ComparePassword(model.OldPassword, User.Identity.Name))
            {
                TempData["WrongMessage"] = "Пароль введен не верно!";
                return RedirectToAction("PasswordView", "TimerClub");
            }

            if(SendDbUtility.ChangePassword(User.Identity.Name, model.NewPassword))
            {
                TempData["SuccessMessage"] = "Пароль успешно изменен!";
                return RedirectToAction("PersonalData", "TimerClub");
            }
            else
            {
                TempData["WrongMessage"] = "Ошибка!";
                return RedirectToAction("PasswordView", "TimerClub");
            }
        }


        public ActionResult RedirectToPassword()
        {
            return RedirectToAction("PasswordView", "TimerClub");
        }

        public ActionResult SendDate(DateModel model)
        {
            //Utilities.SendDbUtility.SendDate(model);
            return RedirectToAction("FindWorkPlaces", "TimerClub");
        }



    }
}