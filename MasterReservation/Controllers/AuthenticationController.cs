using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MasterReservation.Filters;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class AuthenticationController : Controller
    {
        // Регистрация мастера
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterMaster(RegisterMasterModel model)
        {
            //FormsAuthentication.SetAuthCookie("Васильев Вася", false);
            // Если такой емайл уже есть в бд
            if (SendDbUtility.CheckEmail(model.Email))
            {
                TempData["WrongMessage"] = "Такой email уже зарегистрирован!";
                return RedirectToAction("MainPage", "TimerClub");
            }

            // Если такой телефон уже есть в бд
            if (SendDbUtility.CheckPhone(model.PhoneNumber))
            {
                TempData["WrongMessage"] = "Такой номер телефона уже зарегистрирован!";
                return RedirectToAction("MainPage", "TimerClub");
            }

            // Заносим модель в бд
            Utilities.SendDbUtility.SendMaster(model);
            return RedirectToAction("MainPage", "TimerClub");
        }

        //Вход мастера
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginMaster(LoginMaster model)
        {
            if (SendDbUtility.ComparePassword(model.Password, model.Email))
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                var resident = GetData.GetDataResident(model.Email);
                Response.Cookies["ResidentName"].Value = Server.UrlEncode(resident.Name);
                Response.Cookies["ResidentSurname"].Value = Server.UrlEncode(resident.Surname);

                return RedirectToAction("FindWorkPlaces", "TimerClub");
            }
            return RedirectToAction("MainPage", "TimerClub");
        }

        //Вход администратора салона
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginSalon(LoginMaster model)
        {
            if (SendDbUtility.ComparePasswordSalon(model.Password, model.Email))
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                var salon = GetData.GetDataSalonAdmin(model.Email);
                Response.Cookies["ResidentName"].Value = Server.UrlEncode(salon.ContactPerson);
                Response.Cookies["ResidentSurname"].Value = Server.UrlEncode(salon.SalonTitle);
                Response.Cookies["SalonId"].Value = Server.UrlEncode(salon.Id.ToString());

                return RedirectToAction("ShowBookedResidents", "TimerClub");
            }
            return RedirectToAction("MainPage", "TimerClub");
        }

        // Отправка данных для регистрации салона
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendRequestRegisterSalon(RegisterSalonModel model)
        {
            //Utilities.SendDbUtility.SendSalon(model);

            return RedirectToAction("MainPage", "TimerClub");
        }

        [Admin]
        [HttpPost]
        public ActionResult RegisterSalon(SalonModel model)
        {
            Utilities.SendDbUtility.SendSalon(model);
            return RedirectToAction("SalonManage", "Admin");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("MainPage", "TimerClub");
        }

        //Обновление данных о мастере
        public ActionResult UpdateResident(RegisterMasterModel model)
        {
            if (model.Offers == null)
            {
                TempData["WrongMessage"] = "Выберите предоставляемые услуги!";
                return RedirectToAction("PersonalData", "TimerClub");
            }
            //Если пароль введен верно
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


        // Изменение пароля
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


        public ActionResult SendDate(DateModel model)
        {
            //Utilities.SendDbUtility.SendDate(model);
            return RedirectToAction("FindWorkPlaces", "TimerClub");
        }

        //Автодополнение города
        [AllowAnonymous]
        public async Task<ActionResult> GetCities(string term)
        {
            List<string> Cities = new List<string>();

            var x = await Utilities.RequestCity.Request(term);
            x.result.Any();
            var res = x.result.Where(t => t.id != "Free").ToList().Select(t => new
            {
                value = t.name
            });

            return Json(res, JsonRequestBehavior.AllowGet);
        }



    }
}