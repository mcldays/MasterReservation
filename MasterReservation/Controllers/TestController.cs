using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class TestController : Controller
    {
        public ActionResult SignUp(string textbox1)
        {
            ViewBag.Test = "ebat_sobak";
            return View("../Home/About");
            //using (UserContext db = new UserContext())
            //{
            //    db.UsersModel.Add(new UserModel()
            //    {
            //        Name = "1234"
            //    });

            //    db.SaveChanges();
            //}
            //return "hyek";

            //poashe
        }
    }
}