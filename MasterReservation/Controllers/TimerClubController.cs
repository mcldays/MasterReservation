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
            
            object[] x = new object[]
            {
                new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name)),
                new DateModel()
            };
            return View(x);

        }

        public ActionResult WorkingPlacePage()
        {
            return View(new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name)));
        }

        public ActionResult ViewBookedTime()
        {
            return View(new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name)));
        }

        public ActionResult ViewFavorites()
        {
            return View(new RegisterMasterModel(Utilities.GetData.GetDataResident(User.Identity.Name)));
        }
    }
}

