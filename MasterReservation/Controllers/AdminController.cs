using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Filters;

namespace MasterReservation.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Admin]
        public ActionResult SalonManage()
        {
            return View();
        }
    }
}