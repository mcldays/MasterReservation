using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;

namespace MasterReservation.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpPost]
        public ActionResult RegisterMaster(RegisterMasterModel model)
        {
            return View();



        }
    }
}