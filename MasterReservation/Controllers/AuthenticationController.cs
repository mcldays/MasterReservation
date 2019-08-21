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


        public ActionResult LoginMaster(LoginMaster model)
        {

            if (SendDbUtility.CompareAut(model) == true)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
            }

            return RedirectToAction("MainPage", "TimerClub");
        }

    }
}