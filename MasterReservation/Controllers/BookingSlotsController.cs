using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Models;
using MasterReservation.Utilities;

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
                //BookingModel booking = Utilities.SendDbUtility.GetBookingById(Int32.Parse(id));
                //int userSalonId = Int32.Parse(Server.UrlDecode(Request.Cookies["SalonId"].Value));
                //bool salonAdmin = false;
                //List<BookingModel> models = Utilities.SendDbUtility.GetBookingForSalonId(userSalonId);
                //foreach (var model in models)
                //{
                //    if (model.Id == booking.Id)
                //    {
                //        salonAdmin = true;
                //    }
                //}
            
                bool res = Utilities.SendDbUtility.RemoveBooking(Int32.Parse(id), Utilities.SendDbUtility.GetResidentId(User.Identity.Name), false);
                
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

        public bool RemoveBookingAjax(string id)
        {
            try
            {
                bool res = Utilities.SendDbUtility.RemoveBooking(Int32.Parse(id), 0, true);

                if (!res)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string CheckFreeSlotsAjax(string date, string times)
        {
            string res = "";
            DateTime Date = DateTime.Parse(date);
            TimeSpan from = TimeSpan.Parse(times.Split('-')[0]);
            TimeSpan to = TimeSpan.Parse(times.Split('-')[1]);
            List<TimeSlotModel> slots = SendDbUtility.GetAllTimeSlots();
            foreach (var slot in slots)
            {
                TimeSpan slotFrom = TimeSpan.Parse(slot.Time.Split('-')[0]);
                TimeSpan slotTo = TimeSpan.Parse(slot.Time.Split('-')[1]);
                if (Date == slot.Date && slotFrom >= from && slotTo <= to && !slot.Booked)
                {
                    res += slot.PlaceId.ToString() + ",";
                }
            }
            
            return res;
        }
    }
}