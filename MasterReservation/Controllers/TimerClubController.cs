using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterReservation.Controllers
{
    public class TimerClubController : Controller
    {
        // GET: TimerClub
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult PersonalData()
        {
            return View();
        }

        public ActionResult FindWorkPlaces()
        {
            return View();
        }
    }
}