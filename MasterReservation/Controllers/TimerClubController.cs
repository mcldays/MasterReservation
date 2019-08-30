using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
                new RegisterMasterModel(null),
                new RegisterSalonModel()
            };
            return View(x);
        }

        public ActionResult PersonalData()
        {
            return View(new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name)));
        }

        public ActionResult FindWorkPlaces()
        {
            return View();
        }

        public ActionResult WorkingPlacePage()
        {
            return View();
        }

        public ActionResult ViewBookedTime()
        {
            return View();
        }

        public ActionResult ViewFavorites()
        {
            return View();
        }
    }
}

