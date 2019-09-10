using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;

namespace MasterReservation.Controllers
{
    public class BookingSlotsController : Controller
    {
        [HttpPost]
        public ActionResult Booking(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var timesCount = model.Times.Split(';').Length - 1;
                WorkingPlaceModel place = Utilities.SendDbUtility.GetWorkingPlace(model.PlaceId);
                double total;
                if (timesCount < 3)
                {
                    total = timesCount * place.Rate1h;
                }
                else if (timesCount < 12)
                {
                    total = timesCount * place.Rate3h;
                }
                else
                {
                    total = timesCount * place.RateDay;
                }

                model.Sum = total;


                if (!Utilities.SendDbUtility.SetBooking(model))
                {
                    TempData["ErrorMessage"] = "Ошибка бронирования!";
                }
                else
                {
                    TempData["InfoMessage"] = "Рабочее место успешно забронировано!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Не все обязательные поля заполнены!";
            }
            return RedirectToAction("WorkingPlacePage", "TimerClub", new {id = model.PlaceId});
        }

        public ActionResult RemoveBooking(string id)
        {
            try
            {
                bool salonAdmin;
                if (Utilities.SendDbUtility
                    .GetBookingForSalonId(Int32.Parse(Server.UrlDecode(Request.Cookies["SalonId"].Value)))
                    .Contains(Utilities.SendDbUtility.GetBookingById(Int32.Parse(id))))
                {
                    salonAdmin = true;
                }
                else
                {
                    salonAdmin = false;
                }
                bool res = Utilities.SendDbUtility.RemoveBooking(Int32.Parse(id), Utilities.SendDbUtility.GetResidentId(User.Identity.Name), salonAdmin);
                if (!res)
                {
                    TempData["ErrorMessage"] = "Ошибка удаления!";
                }
                else
                {
                    TempData["InfoMessage"] = "Запись успешно удалена!";
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Ошибка удаления!";
            }
            
            return RedirectToAction("ViewBookedTime", "TimerClub");
        }
    }
}