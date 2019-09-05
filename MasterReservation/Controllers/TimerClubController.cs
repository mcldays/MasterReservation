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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("FindWorkPlaces");
            }

            ViewBag.wrongMessage = TempData["WrongMessage"];

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
            ViewBag.wrongMessage = TempData["WrongMessage"];
            ViewBag.successMessage = TempData["SuccessMessage"];
            RegisterMasterModel model = new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name));
            model.Password = string.Empty;
            return View(model);
        }

        public ActionResult FindWorkPlaces()
        {
            object[] x = new object[]
            {
                new DateModel()
            };
            return View(x);

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

        public ActionResult PasswordView()
        {
            ViewBag.wrongMessage = TempData["WrongMessage"];
            return View();
        }
    }
}

