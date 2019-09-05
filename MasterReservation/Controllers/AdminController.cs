using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterReservation.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult SalonManage()
        {
            if (User.Identity.Name != "lexlex971@mail.ru")
            {
                return RedirectToAction("MainPage", "TimerClub");
            }
            return View();
        }
    }
}