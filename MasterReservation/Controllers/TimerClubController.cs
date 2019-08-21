﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;

namespace MasterReservation.Controllers
{
    [AllowAnonymous]
    public class TimerClubController : Controller
    {
        // GET: TimerClub
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
            return View();
        }
    }
}