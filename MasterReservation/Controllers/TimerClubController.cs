using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using MasterReservation.Models;
using MasterReservation.Utilities;
using Microsoft.Ajax.Utilities;

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

            if (model.Picture == null)
            {
                Utilities.SendDbUtility.SendPictureToDb(model);
            }


            model.Password = string.Empty;
            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                ViewBag.IsAdminOfSalon = true;
            }
            else
            {
                ViewBag.IsAdminOfSalon = false;
            }
            return View(model);
        }

        public ActionResult FindWorkPlaces()
        {
            List<string> titles = new List<string>();
            List<string> addreses = new List<string>();
            List<WorkingPlaceModel> places = Utilities.SendDbUtility.GetAllWorkingPlaces();
            List<bool> favorites = new List<bool>();
            
            ResidentModel resident = Utilities.SendDbUtility.GetResident(User.Identity.Name);
            int userId = 0;
            string userCity = "";
            string userOffers = "";
            if (!Request.Cookies.AllKeys.Contains("SalonId"))
            {
                if (resident == null)
                {
                    return RedirectToAction("LogOut", "Authentication");
                }
                userId = resident.Id;
                userCity = resident.City;
                userOffers = resident.Offers;
            }
            else
            {
                places.RemoveAll(t => t.SalonId.ToString() != Request.Cookies["SalonId"].Value);

                favorites.ForEach(t => t = false);
            }
            
            
            List<SalonModel> salons = new List<SalonModel>();

            foreach (var place in places)
            {
                titles.Add(Utilities.SendDbUtility.GetSalonTitle(place.SalonId));
                addreses.Add(Utilities.SendDbUtility.GetSalonAdress(place.SalonId));
                favorites.Add(Utilities.SendDbUtility.CheckFavorite(place.Id, userId));
                salons.Add(Utilities.SendDbUtility.GetSalon(place.SalonId));
            }


                object[] x = new object[]
            {
                new DateModel(),
                places,
                titles,
                addreses,
                favorites,
                salons,
                userCity,
                userOffers
            };
            return View(x);

        }

        public ActionResult WorkingPlacePage(string id)
        {
            if (id == null || !id.IsInt())
            {
                return RedirectToAction("FindWorkPlaces");
            }

            WorkingPlaceModel modelPlace = Utilities.SendDbUtility.GetWorkingPlace(Int32.Parse(id));
            if (modelPlace == null)
            {
                return RedirectToAction("FindWorkPlaces");
            }

            SalonModel modelSalon = Utilities.SendDbUtility.GetSalon(modelPlace.SalonId);
            List<TimeSlotModel> modelsTime = Utilities.SendDbUtility.GetTimeSlots(modelPlace.Id);
            if (modelSalon == null)
            {
                return RedirectToAction("FindWorkPlaces");
            }

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.InfoMessage = TempData["InfoMessage"];

            bool isAdminOfSalon = false;
            int userId = 0;
            if (Request.Cookies.AllKeys.Contains("SalonId") && Request.Cookies["SalonId"].Value == modelPlace.SalonId.ToString() && Utilities.SendDbUtility.CheckAdmin(modelPlace.SalonId, User.Identity.Name))
            {
                isAdminOfSalon = true;
            }
            else
            {
                userId = Utilities.SendDbUtility.GetResidentId(User.Identity.Name);
            }

            ViewBag.Favorites = Utilities.SendDbUtility.CheckFavorite(modelPlace.Id, userId);

            ViewBag.IsAdminOfSalon = isAdminOfSalon;

            BookingModel modelBooking = new BookingModel()
            {
                PlaceId = Int32.Parse(id),
                SalonId = modelPlace.SalonId,
                ResidentId = userId
            };

           

            object[] x = new object[]
            {
                modelPlace,
                modelSalon,
                modelsTime,
                modelBooking
            };

            return View(x);
        }


        public ActionResult ViewBookedTime()
        {
            List<string> titles = new List<string>();
            List<BookingModel> models =
                Utilities.SendDbUtility.GetBooking(Utilities.SendDbUtility.GetResidentId(User.Identity.Name));
            List<int> placesTimes = new List<int>();
            foreach (var model in models)
            {
                titles.Add(Utilities.SendDbUtility.GetSalonTitle(model.SalonId));
                placesTimes.Add(Utilities.SendDbUtility.GetCountTimesOfDay(model.Date, model.PlaceId));
            }

            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                titles.Clear();
                models.Clear();
                placesTimes.Clear();
            }

            object[] x = new object[]
            {
                models,
                titles,
                placesTimes
            };

            


            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.InfoMessage = TempData["InfoMessage"];

            return View(x);
        }

        public ActionResult ViewFavorites()
        {

            List<FavoriteModel> models = new List<FavoriteModel>();
            int userId = Utilities.SendDbUtility.GetResidentId(User.Identity.Name);
            
            string[] favorites = Utilities.SendDbUtility.GetFavorites(userId).Split(',');
            for (int i = 0; i < favorites.Length - 1; i++)
            {
                WorkingPlaceModel place = Utilities.SendDbUtility.GetWorkingPlace(Int32.Parse(favorites[i]));
                SalonModel salon;

                try
                {
                    salon = SendDbUtility.GetSalon(place.SalonId);
                    models.Add(new FavoriteModel()
                    {
                        PlaceId = Int32.Parse(favorites[i]),
                        SalonTitle = salon.SalonTitle,
                        SalonAdress = salon.Adress,
                        Rate1h = place.Rate1h,
                        Rateday = place.RateDay,
                        RateHalfMounth = place.RateHalfMounth,
                        RateMounth = place.RateMounth
                    });
                }
                catch (Exception e)
                {
                    
                }
            }

            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                models.Clear();
            }


            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.InfoMessage = TempData["InfoMessage"];

            return View(models);
        }

        public ActionResult PasswordView()
        {
            ViewBag.wrongMessage = TempData["WrongMessage"];
            return View();
        }

        public ActionResult ShowBookedResidents()
        {
            if (!Request.Cookies.AllKeys.Contains("SalonId"))
            {
                return RedirectToAction("MainPage");
            }
            int salonId = Int32.Parse(Server.UrlDecode(Request.Cookies["SalonId"].Value));

            if (!Utilities.SendDbUtility.CheckAdmin(salonId, User.Identity.Name))
            {
                return RedirectToAction("MainPage");
            }



            
            
            //List<BookingModel> bookings = Utilities.SendDbUtility.GetBookingForSalonId(salonId);
            //List<ResidentModel> residents = new List<ResidentModel>();
            //foreach (var a in residents)
            //{
            //    List<BookingModel> timeslots = Utilities.SendDbUtility.GetBooking(a.Id);
            //}
           

            //foreach (var booking in bookings)
            //{
            //    residents.AddRange(allResidents.Where(t=>t.Id == booking.ResidentId));
            //}


            SalonModel salon = Utilities.SendDbUtility.GetSalon(salonId);
            if (salon == null)
            {
                return RedirectToAction("MainPage");
            }
            List<List<int>> workingTimes = new List<List<int>>();
            workingTimes.Add(new List<int>() { Int32.Parse(salon.OperatingModeSun.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeSun.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeMon.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeMon.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeTue.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeTue.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeWed.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeWed.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeThu.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeThu.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeFri.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeFri.Split('-')[1].Split(':')[0]) + 1 });
            workingTimes.Add(new List<int>(){ Int32.Parse(salon.OperatingModeSat.Split('-')[0].Split(':')[0]), Int32.Parse(salon.OperatingModeSat.Split('-')[1].Split(':')[0]) + 1 });
            foreach (var times in workingTimes)
            {
                if (times[1] - times[0] == 1)
                {
                    times[1] = times[0];
                }
            }

            List<WorkingPlaceModel> places = Utilities.SendDbUtility.GetWorkingPlaces(salonId);


            List<BookingModel> bookings = Utilities.SendDbUtility.GetBookingForSalonId(salonId);

            List<ResidentModel> allResidents = Utilities.SendDbUtility.GetAllResidents();

            object[] x = new object[]
            {
                //bookings,
                //allResidents
                workingTimes,
                places,
                bookings,
                allResidents
            };

           // AdminBookingModel model = new AdminBookingModel();

           //model.JobPlaceId =  Int32.Parse(Request.Cookies["SalonId"].Value);

           // int SalonIdAdmin = Int32.Parse(Request.Cookies["SalonId"].Value);

           // // получаем список рабочих мест по айди из куки
           // model.bookingTimeList = Utilities.SendDbUtility.GetWorkingPlaces(SalonIdAdmin);
            


            //foreach (var f in model.bookingTimeList)
            //{
            //  model.TimeSlotAdmin.AddRange(Utilities.SendDbUtility.GetTimeSlots(f.Id));

            //}
           
           



            return View(x);
        }
    }
}

