using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult UpdateResident(ResidentModel model)
        {
            if (Utilities.SendDbUtility.ComparePassword(model) == true)
            {

                Utilities.SendDbUtility.ChangeResident(model);


            }

            return RedirectToAction("PersonalData", "TimerClub");
            
         

        }
      

    }
}