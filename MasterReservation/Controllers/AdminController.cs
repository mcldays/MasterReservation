using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MasterReservation.Filters;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class AdminController : Controller
    {

        [Admin]
        public ActionResult SalonShow()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            List<SalonModel> models = Utilities.SendDbUtility.GetAllSalons();

            object[] x = new object[]
            {
                models,
                new SalonModel()
            };
            return View(x);
        }

        [Admin]
        public ActionResult SalonPlaces(string id)
        {

            if (id == null)
            {
                return RedirectToAction("SalonShow");
            }

            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            List<WorkingPlaceModel> models = Utilities.SendDbUtility.GetWorkingPlaces(Int32.Parse(id));
            ViewBag.SalonTitle = Utilities.SendDbUtility.GetSalonTitle(Int32.Parse(id));
            object[] x = new object[]
            {
                models,
                new WorkingPlaceModel()
                {
                    SalonId = Int32.Parse(id)
                }
                
               
            };
            //List<SalonModel> models = Utilities.SendDbUtility.GetAllSalons();
            return View(x);
        }
        






        [Admin]
        [HttpPost]
        public ActionResult AddWorkingPlace(WorkingPlaceModel model, IEnumerable<HttpPostedFileBase> SalonPhoto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Не заполнены все обязательные поля!";
                return RedirectToAction("SalonPlaces", "Admin", new { id = model.SalonId });
            }

            Utilities.SendDbUtility.AddWorkingPlace(model);
            foreach (var file in SalonPhoto)
            {
                if (file != null)
                {
                    SendDbUtility.SaveSalonPhoto(file, model.Id.ToString());

                }
            }
            return RedirectToAction("SalonPlaces","Admin", new { id = model.SalonId });
        }

        [Admin]
        public string RemoveWorkingPlace(int placeId)
        {
            Utilities.SendDbUtility.RemoveWorkingPlace(placeId);
            return "Рабочее место удалено!";
        }

        [Admin]
        [HttpPost]
        public ActionResult AddSalon(SalonModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Не заполнены все обязательные поля!";
                return RedirectToAction("SalonShow");
            }
            Utilities.SendDbUtility.AddSalon(model);
            return RedirectToAction("SalonShow");
        }

        [Admin]
        public string RemoveSalon(int salonId)
        {
            Utilities.SendDbUtility.RemoveSalon(salonId);
            return "Салон удален!";
        }
    }
}