using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.hgfhgfh = "vhgvhg";
            ViewBag.Test = "";
            return View();
        }

        public ActionResult SignUp(string surname, string firstname,
            string patronymic, string phone, string email, string servicesids,
            string studyplace, string experience, string awards)
        {
            ViewBag.Test = "ebat_sobak";

            using (UserContext db = new UserContext())
            {
                db.ResidentModels.Add(new ResidentModel()
                {
                    Surname = surname,
                    Name = firstname,
                    Patronymic = patronymic,
                    Phone = phone,
                    Email = email,
                    ServicesIds = servicesids,
                    StudyPlace = studyplace,
                    Experience = experience,
                    Awards = awards
                });
                db.SaveChanges();
            }


            List<ResidentModel> Data = new List<ResidentModel>();
            using (UserContext db = new UserContext())
            {
                Data = db.ResidentModels.ToList();
            }


            return View(model: Data);
            //using (UserContext db = new UserContext())
            //{
            //    db.UsersModel.Add(new UserModel()
            //    {
            //        Name = "1234"
            //    });

            //    db.SaveChanges();
            //}
            //return "hyek";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}