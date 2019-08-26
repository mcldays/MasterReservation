using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;

namespace MasterReservation.Controllers
{
   
    public class TimerClubController : Controller
    {
        // GET: TimerClub
        [AllowAnonymous]
        public ActionResult MainPage()
        {
            ViewBag.LoginModel = new LoginMaster();
            object[] x = new object[]
            {
                new LoginMaster(),
                new RegisterMasterModel(),
                new RegisterSalonModel()
            };
            return View(x);
        }

        public ActionResult PersonalData()
        {


            return View(Utilities.GetData.GetDataResident(User.Identity.Name));
        }
    }
}

