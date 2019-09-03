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
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpPost]
        public ActionResult RegisterMaster(RegisterMasterModel model)
        {
            FormsAuthentication.SetAuthCookie("Васильев Вася", false);
            
                
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

                return RedirectToAction("PersonalData", "TimerClub");

            }

            return View();
        }


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
          
                Utilities.SendDbUtility.ChangeResident(model);
            

            return RedirectToAction("PersonalData", "TimerClub");
           
        }

        
        public JsonResult CheckPassword(string Password)
        {
            var result = Utilities.SendDbUtility.ComparePassword(Password, User.Identity.Name);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ChangePasswordModel ChangePassword(ChangePasswordModel model)
        {
            return null;
        }


        [HttpPost]
        public ActionResult PasswordChange()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            if (Utilities.SendDbUtility.ChangePassword(User.Identity.Name, model) == true)
            {
               return RedirectToAction("PersonalData", "TimerClub");
            }
            else return RedirectToAction("MainPage", "TimerClub");

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