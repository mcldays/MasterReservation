using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class UtilitiesController : Controller
    {
        // GET: Utilities
        public ActionResult Create(byte[] Image, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {


                UserContext db = new UserContext();


                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                Image = imageData;

              

              
                

                return RedirectToAction("FindWorkPlaces", "TimerClub");
            }
            return RedirectToAction("FindWorkPlaces", "TimerClub");
        }

    }
}