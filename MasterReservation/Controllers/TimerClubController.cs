using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using MasterReservation.Models;
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
            int userId = Utilities.SendDbUtility.GetResidentId(User.Identity.Name);
            List<SalonModel> salons = new List<SalonModel>();

            if (places == null)
            {
                places = new List<WorkingPlaceModel>();
            }
            foreach (var place in places)
            {
                titles.Add(Utilities.SendDbUtility.GetSalonTitle(place.SalonId));
                addreses.Add(Utilities.SendDbUtility.GetSalonAdress(place.SalonId));
                favorites.Add(Utilities.SendDbUtility.CheckFavorite(place.Id, userId));
                salons.Add(Utilities.SendDbUtility.GetSalon(place.SalonId));
            }

            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                //for (int i = 0; i < favorites.Count; i++)
                //{
                //    favorites[i] = false;
                //}
                favorites.ForEach(t=>t = false);
            }

                object[] x = new object[]
            {
                new DateModel(),
                places,
                titles,
                addreses,
                favorites,
                salons
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
            if (modelSalon == null || modelsTime.Count == 0)
            {
                return RedirectToAction("FindWorkPlaces");
            }

            int userId = Utilities.SendDbUtility.GetResidentId(User.Identity.Name);

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.InfoMessage = TempData["InfoMessage"];

            ViewBag.Favorites = Utilities.SendDbUtility.CheckFavorite(modelPlace.Id, userId);

            BookingModel modelBooking = new BookingModel()
            {
                PlaceId = Int32.Parse(id),
                SalonId = modelPlace.SalonId,
                ResidentId = userId
            };

            bool isAdminOfSalon = false;

            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                isAdminOfSalon = true;
            }

            ViewBag.IsAdminOfSalon = isAdminOfSalon;

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
            foreach (var model in models)
            {
                titles.Add(Utilities.SendDbUtility.GetSalonTitle(model.SalonId));
            }

            if (Request.Cookies.AllKeys.Contains("SalonId"))
            {
                titles.Clear();
                models.Clear();
            }

            object[] x = new object[]
            {
                models,
                titles
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
                SalonModel salon = Utilities.SendDbUtility.GetSalon(place.SalonId);
                models.Add(new FavoriteModel()
                {
                    PlaceId = Int32.Parse(favorites[i]),
                    SalonTitle = salon.SalonTitle,
                    SalonAdress = salon.Adress,
                    Rate1h = place.Rate1h,
                    Rate3h = place.Rate3h,
                    Rateday = place.RateDay
                });
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
            List<ResidentModel> allResidents = Utilities.SendDbUtility.GetAllResidents();
            int salonId = Int32.Parse(Server.UrlDecode(Request.Cookies["SalonId"].Value));
            List<BookingModel> models = Utilities.SendDbUtility.GetBookingForSalonId(salonId);
            List<ResidentModel> residents = new List<ResidentModel>();

            foreach (var booking in models)
            {
                residents.AddRange(allResidents.Where(t=>t.Id == booking.ResidentId));
            }

            object[] x = new object[]
            {
                models,
                allResidents
            };
            return View(x);
        }
    }
}

