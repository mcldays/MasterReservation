﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using MasterReservation.Models;
using System.IO;
using System.Text;
using System.Web.Hosting;


namespace MasterReservation.Utilities
{
    public static class SendDbUtility
    {

        //Метод занесения данных мастера в бд
        public static bool SendMaster(RegisterMasterModel model)
        {
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    DbUse.ResidentModels.Add(new ResidentModel()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        Email = model.Email,
                        Phone = model.PhoneNumber,
                        City = model.City,
                        Offers = model.Offers,
                        Experience = model.Expirience,
                        Awards = model.Awards,
                        Password = GetData.GetHash(model.Password),
                        IsAdmin = false
                    });
                    DbUse.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return true;
            }
            return true;
        }

        // Метод занесения данных салона в бд
        public static bool SendSalon(SalonModel model)
        {
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    DbUse.SalonModels.Add(model);
                    DbUse.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return true;
            }
            return true;
        }

        //Получить данные всех салонов
        public static List<SalonModel> GetAllSalons()
        {
            List<SalonModel> models = new List<SalonModel>();
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    models = DbUse.SalonModels.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return models;
        }

        //Находим рабочие места конкретного салона
        public static List<WorkingPlaceModel> GetWorkingPlaces(int SalonId)
        {
            List<WorkingPlaceModel> models = new List<WorkingPlaceModel>();
            try
            {
                using (UserContext DbUse = new UserContext())
                {
                    models = DbUse.WorkingPlaceModels.Where(t=>t.SalonId == SalonId).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return models;
        }

        // Метод изменения данных мастера в бд
        public static bool ChangeResident(RegisterMasterModel model)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Id == model.id);

                    if (user == null) return false;
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Awards = model.Awards;
                    user.Offers = model.Offers;
                    user.Phone = model.PhoneNumber;
                    user.Patronymic = model.Patronymic;
                    user.Experience = model.Expirience;
                    dbUse.ResidentModels.AddOrUpdate(user);
                    dbUse.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Метод проверки введенного логина и пароля
        public static bool ComparePassword(string Password, string Email)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    var hashPass = GetData.GetHash(Password);
                    ResidentModel user =
                        dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email && t.Password == hashPass);
                
                    if (user == null) return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Метод проверки введенного логина и пароля салона
        public static bool ComparePasswordSalon(string Password, string Email)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    //var hashPass = GetData.GetHash(Password);
                    SalonModel user =
                        dbUse.SalonModels.FirstOrDefault(t => t.Email == Email && t.AdminPass == Password);

                    if (user == null) return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Метод изменения пароля
        public static bool ChangePassword(string Email, string newPass)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                    if (user == null) return false;
                    user.Password = GetData.GetHash(newPass);
                    dbUse.ResidentModels.AddOrUpdate(user);
                    dbUse.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Метод проверки существования емэйла в бд
        public static bool CheckEmail(string Email)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == Email);
                    if (user == null) return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }
            
        }

        // Метод проверки существования номера телефона в бд
        public static bool CheckPhone(string Phone)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Phone == Phone);
                    if (user == null) return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }
            
        }

        public static bool IsAdmin(string email)
        {
            try
            {
                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Email == email && t.IsAdmin);
                    if (user == null) return false;
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool AddWorkingPlace(WorkingPlaceModel model)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    dbUse.WorkingPlaceModels.Add(model);
                    dbUse.SaveChanges();

                    SalonModel salon = GetSalon(model.SalonId);
                    string[] workingTimeMon = salon.OperatingModeMon.Split('-');
                    int constFromMon = Int32.Parse(workingTimeMon[0].Trim().Substring(0, 2));
                    int constToMon = Int32.Parse(workingTimeMon[1].Trim().Substring(0, 2));

                    string[] workingTimeTue = salon.OperatingModeTue.Split('-');
                    int constFromTue = Int32.Parse(workingTimeTue[0].Trim().Substring(0, 2));
                    int constToTue = Int32.Parse(workingTimeTue[1].Trim().Substring(0, 2));

                    string[] workingTimeWed = salon.OperatingModeWed.Split('-');
                    int constFromWed = Int32.Parse(workingTimeWed[0].Trim().Substring(0, 2));
                    int constToWed = Int32.Parse(workingTimeWed[1].Trim().Substring(0, 2));

                    string[] workingTimeThu = salon.OperatingModeThu.Split('-');
                    int constFromThu = Int32.Parse(workingTimeThu[0].Trim().Substring(0, 2));
                    int constToThu = Int32.Parse(workingTimeThu[1].Trim().Substring(0, 2));

                    string[] workingTimeFri = salon.OperatingModeFri.Split('-');
                    int constFromFri = Int32.Parse(workingTimeFri[0].Trim().Substring(0, 2));
                    int constToFri = Int32.Parse(workingTimeFri[1].Trim().Substring(0, 2));

                    string[] workingTimeSat = salon.OperatingModeSat.Split('-');
                    int constFromSat = Int32.Parse(workingTimeSat[0].Trim().Substring(0,2));
                    int constToSat = Int32.Parse(workingTimeSat[1].Trim().Substring(0,2));

                    string[] workingTimeSun = salon.OperatingModeSun.Split('-');
                    int constFromSun = Int32.Parse(workingTimeSun[0].Trim().Substring(0, 2));
                    int constToSun = Int32.Parse(workingTimeSun[1].Trim().Substring(0, 2));
                    if (constFromMon > constToMon)
                    {
                        constFromMon = constToMon;
                    }
                    if (constFromTue > constToTue)
                    {
                        constFromTue = constToTue;
                    }
                    if (constFromWed > constToWed)
                    {
                        constFromWed = constToWed;
                    }
                    if (constFromThu > constToThu)
                    {
                        constFromThu = constToThu;
                    }
                    if (constFromFri > constToFri)
                    {
                        constFromFri = constToFri;
                    }


                    if (constFromSat > constToSat)
                    {
                        constFromSat = constToSat;
                    }
                    if (constFromSun > constToSun)
                    {
                        constFromSun = constToSun;
                    }

                    int from;
                    int to;
                    int tempTo;
                    int tempFrom;
                    DateTime dateNow = DateTime.Today;
                    for (int i = 0; i < 14; i++)
                    {
                        
                        if (dateNow.DayOfWeek == DayOfWeek.Saturday)
                        {
                            from = constFromSat;
                            to = constToSat;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Sunday)
                        {
                            from = constFromSun;
                            to = constToSun;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Monday)
                        {
                            from = constFromMon;
                            to = constToMon;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            from = constFromTue;
                            to = constToTue;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            from = constFromWed;
                            to = constToWed;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Thursday)
                        {
                            from = constFromThu;
                            to = constToThu;
                        }
                        else if (dateNow.DayOfWeek == DayOfWeek.Friday)
                        {
                            from = constFromFri;
                            to = constToFri;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                        while (from != to)
                        {
                            tempFrom = from;
                            tempTo = ++from;
                            dbUse.TimeSlotModels.Add(new TimeSlotModel()
                            {
                                PlaceId = model.Id,
                                SalonId = model.SalonId,
                                Time = tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00",
                                Booked = false,
                                Date = dateNow
                            });
                        }
                        
                        dateNow = dateNow.AddDays(1);
                    }
                    dbUse.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool RemoveWorkingPlace(int id)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    dbUse.WorkingPlaceModels.Remove(dbUse.WorkingPlaceModels.Where(t => t.Id == id).FirstOrDefault());
                    dbUse.TimeSlotModels.RemoveRange(dbUse.TimeSlotModels.Where(t => t.PlaceId == id));
                    dbUse.BookingModels.RemoveRange(dbUse.BookingModels.Where(t => t.PlaceId == id));
                    dbUse.SaveChanges();
                    RemovePictures(id);
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool AddSalon(SalonModel model)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    dbUse.SalonModels.Add(model);
                    dbUse.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool RemoveSalon(int salonId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    var places = GetWorkingPlaces(salonId);
                    foreach (var place in places)
                    {
                        RemoveWorkingPlace(place.Id);
                    }
                    dbUse.SalonModels.Remove(dbUse.SalonModels.Where(t => t.Id == salonId).FirstOrDefault());
                    dbUse.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }

        public static string GetSalonTitle(int salonId)
        {
            using (UserContext dbUse = new UserContext())
            {
                string salonTitle;
                try
                {
                    salonTitle = dbUse.SalonModels.FirstOrDefault(t => t.Id == salonId).SalonTitle;
                }
                catch (Exception e)
                {
                    return null;
                }

                return salonTitle;
            }
        }

        public static string GetSalonAdress(int salonId)
        {
            using (UserContext dbUse = new UserContext())
            {
                string salonAdress;
                try
                {
                    salonAdress = dbUse.SalonModels.FirstOrDefault(t => t.Id == salonId).Adress;
                }
                catch (Exception e)
                {
                    return null;
                }

                return salonAdress;
            }
        }

        public static WorkingPlaceModel GetWorkingPlace(int id)
        {
            WorkingPlaceModel model;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    model = dbUse.WorkingPlaceModels.FirstOrDefault(t => t.Id == id);
                    return model;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static SalonModel GetSalon(int id)
        {
            SalonModel model;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    model = dbUse.SalonModels.FirstOrDefault(t => t.Id == id);
                    return model;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static List<TimeSlotModel> GetTimeSlots(int placeId)
        {
            List<TimeSlotModel> models;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    models = dbUse.TimeSlotModels.Where(t => t.PlaceId == placeId).ToList();
                    return models;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static List<TimeSlotModel> GetTimeSlotsBySalonId(int salonId)
        {
            List<TimeSlotModel> models;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    models = dbUse.TimeSlotModels.Where(t => t.SalonId == salonId).ToList();
                    return models;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static int GetResidentId(string email)
        {
            int id;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    id = dbUse.ResidentModels.FirstOrDefault(t => t.Email == email).Id;
                    return id;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public static ResidentModel GetResident(string email)
        {
            ResidentModel resident;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    resident = dbUse.ResidentModels.FirstOrDefault(t => t.Email == email);
                    return resident;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static bool SetBooking(BookingModel model)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    List<BookingModel> bookings = dbUse.BookingModels.ToList();
                    foreach (var book in bookings)
                    {
                        bool flagBooked = false;
                        string[] bookTimes = book.Times.Split(';');
                        for (int i = 0; i < bookTimes.Length-1; i++)
                        {
                            if (model.Times.Contains(bookTimes[i]))
                            {
                                flagBooked = true;
                                break;
                            }
                        }

                        if ((book.PlaceId == model.PlaceId) && (book.Date == model.Date) && (flagBooked))
                        {
                            return false;
                        }
                    }
                    SalonModel salon = GetSalon(model.SalonId);
                    if (salon.ReservationType)
                    {
                        model.Confirmed = false;
                    }
                    else
                    {
                        model.Confirmed = true;
                    }
                    BookingModel booking = dbUse.BookingModels.Add(model);
                    
                    dbUse.SaveChanges();

                    string[] times = model.Times.Split(';');
                    TimeSlotModel timeSlot;
                    string chosenTime;
                    for (int i = 0; i < times.Length-1; i++)
                    {
                        chosenTime = times[i];
                        timeSlot = dbUse.TimeSlotModels.FirstOrDefault(t =>
                            ((t.Time == chosenTime) && (t.Date == model.Date) && (t.PlaceId == model.PlaceId)));
                        if (timeSlot.Booked || timeSlot == null)
                        {
                            return false;
                        }
                        timeSlot.ResidentId = model.ResidentId;
                        timeSlot.Booked = true;
                        timeSlot.BookingId = booking.Id;
                        dbUse.TimeSlotModels.AddOrUpdate(timeSlot);
                    }
                        

                    
                    dbUse.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static List<BookingModel> GetBooking(int residentId)
        {
            List<BookingModel> models;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    models = dbUse.BookingModels.Where(t => t.ResidentId == residentId).ToList();
                    return models;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static BookingModel GetBookingById(int bookingId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    return dbUse.BookingModels.Where(t => t.Id == bookingId).FirstOrDefault();;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }




        public static List<BookingModel> GetBookingForSalonId(int salonId)
        {
            List<BookingModel> models;
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    models = dbUse.BookingModels.Where(t => t.SalonId == salonId).ToList();
                    return models;
                }
                catch (Exception e)
                {
                    return new List<BookingModel>();
                }
            }
        }

        

        public static bool RemoveBooking(int bookingId, int residentId, bool salonAdmin)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    if (salonAdmin)
                    {
                        dbUse.BookingModels.Remove(dbUse.BookingModels.Where(t => t.Id == bookingId).FirstOrDefault());
                    }
                    else
                    {
                        dbUse.BookingModels.Remove(dbUse.BookingModels.Where(t => t.Id == bookingId && t.ResidentId == residentId).FirstOrDefault());
                    }

                    var slots = dbUse.TimeSlotModels.Where(t => t.BookingId == bookingId);
                    foreach (var slot in slots)
                    {
                        slot.Booked = false;
                        slot.BookingId = 0;
                        slot.ResidentId = 0;
                        dbUse.TimeSlotModels.AddOrUpdate(slot);
                    }
                    
                    dbUse.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool SetFavorite(int placeId, int residentId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    ResidentModel model = dbUse.ResidentModels.FirstOrDefault(t => t.Id == residentId);

                    if (model.Favorites != null && model.Favorites.Contains(placeId.ToString() + ","))
                    {
                        return false;
                    }
                    model.Favorites = model.Favorites + placeId.ToString() + ",";
                    dbUse.ResidentModels.AddOrUpdate(model);
                    dbUse.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool RemoveFavorite(int placeId, int residentId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    ResidentModel model = dbUse.ResidentModels.FirstOrDefault(t => t.Id == residentId);
                    if (!model.Favorites.Contains(placeId.ToString() + ","))
                    {
                        return false;
                    }
                    model.Favorites = model.Favorites.Replace(placeId.ToString() + ",", "");
                    dbUse.ResidentModels.AddOrUpdate(model);
                    dbUse.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool CheckFavorite(int placeId, int residentId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    ResidentModel model = dbUse.ResidentModels.FirstOrDefault(t => t.Id == residentId);
                    if (!model.Favorites.Contains(placeId.ToString() + ","))
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static string GetFavorites(int residentId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    string favorites = dbUse.ResidentModels.FirstOrDefault(t => t.Id == residentId).Favorites;
                    if (favorites != null)
                    {
                        return favorites;
                    }
                    return "";
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public static List<WorkingPlaceModel> GetAllWorkingPlaces()
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    var places = dbUse.WorkingPlaceModels.ToList();

                    return places;
                }
                catch (Exception e)
                {
                    return new List<WorkingPlaceModel>();
                }
            }
        }

        public static List<ResidentModel> GetAllResidents()
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    return dbUse.ResidentModels.ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static List<TimeSlotModel> GetAllTimeSlots()
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    return dbUse.TimeSlotModels.ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static string CheckTimeSlot(DateTime date, string times, int placeId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    TimeSlotModel model = dbUse.TimeSlotModels.FirstOrDefault(t => t.PlaceId == placeId && t.Date == date && t.Time == times);
                    if (model == null)
                    {
                        return "0";
                    }
                    else
                    {
                        return "1";
                    }
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }

        public static int GetCountTimesOfDay(DateTime date, int placeId)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    return dbUse.TimeSlotModels.Where(t=>t.Date == date && t.PlaceId == placeId).ToList().Count;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }

        public static bool CheckAdmin(int salonId, string email)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    return dbUse.SalonModels.FirstOrDefault(t=>t.Email == email && t.Id == salonId) != null;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }



        //public static bool SendDate(DateModel model)
        //{
        //    try
        //    {
        //        using (UserContext DbUse = new UserContext())
        //        {
        //            DbUse.DateModels.AddOrUpdate(model);
        //             DbUse.SaveChanges();
        //            return true;
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        return true;
        //    }
        //}
        public static bool SaveSalonPhoto(HttpPostedFileBase file, string id)
        {
            try
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(file.FileName);

                string path = HostingEnvironment.ApplicationHost.GetPhysicalPath() + @"\SalonPhoto";
                string subpath = id;
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                DirectoryInfo dirInfoTumbnail = new DirectoryInfo(path + @"\Thumbnails");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                if (!dirInfoTumbnail.Exists)
                {
                    dirInfoTumbnail.Create();
                }
                dirInfo.CreateSubdirectory(subpath);
                dirInfoTumbnail.CreateSubdirectory(subpath);

                file.SaveAs(path + @"\" + subpath + @"\" + fileName);

                ResizeImage(path + @"\" + subpath + @"\" + fileName,path + @"\Thumbnails\" + subpath + @"\" + fileName,210, 216,ImageFormat.Jpeg );
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool RemovePictures(int placeId)
        {
            try
            {
                string path = HostingEnvironment.ApplicationHost.GetPhysicalPath() + $@"\SalonPhoto\{placeId}";
                string pathThumbnails = HostingEnvironment.ApplicationHost.GetPhysicalPath() + $@"\SalonPhoto\Thumbnails\{placeId}";

                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo dirThumbnails = new DirectoryInfo(pathThumbnails);

                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
                foreach (FileInfo file in dirThumbnails.GetFiles())
                {
                    file.Delete();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CreatePicture(byte[] picture, int id)
        {
            try
            {
                string path = HostingEnvironment.ApplicationHost.GetPhysicalPath() + $@"\ResidenAvatar\{id.ToString()}.jpg";

                File.WriteAllBytes(path, picture);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendPictureToDb(RegisterMasterModel model)
        {
            try
            {
                if (model != null)
                {
                    string path =
                        HostingEnvironment.ApplicationHost.GetPhysicalPath() + $@"\ResidenAvatar\{model.id.ToString()}.jpg";
                    //model.Picture = RotateImage(File.ReadAllBytes(path));
                    model.Picture = File.ReadAllBytes(path);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public static void ResizeImage(string FileNameInput, string FileNameOutput, double ResizeHeight, double ResizeWidth, ImageFormat OutputFormat)
        {
            using (System.Drawing.Image photo = new Bitmap(FileNameInput))
            {
                double aspectRatio = (double)photo.Width / photo.Height;
                double boxRatio = ResizeWidth / ResizeHeight;
                double scaleFactor = 0;

                if (photo.Width < ResizeWidth && photo.Height < ResizeHeight)
                {
                    // keep the image the same size since it is already smaller than our max width/height
                    scaleFactor = 1.0;
                }
                else
                {
                    if (boxRatio > aspectRatio)
                        scaleFactor = ResizeHeight / photo.Height;
                    else
                        scaleFactor = ResizeWidth / photo.Width;
                }

                int newWidth = (int)(photo.Width * scaleFactor);
                int newHeight = (int)(photo.Height * scaleFactor);

                using (Bitmap bmp = new Bitmap(newWidth, newHeight))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        g.DrawImage(photo, 0, 0, newWidth, newHeight);

                        if (ImageFormat.Png.Equals(OutputFormat))
                        {
                            bmp.Save(FileNameOutput, OutputFormat);
                        }
                        else if (ImageFormat.Jpeg.Equals(OutputFormat))
                        {
                            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                            EncoderParameters encoderParameters;
                            using (encoderParameters = new System.Drawing.Imaging.EncoderParameters(1))
                            {
                                // use jpeg info[1] and set quality to 90
                                encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
                                try
                                {
                                    PropertyItem propItem = photo.GetPropertyItem(274);
                                    bmp.SetPropertyItem(propItem);
                                }
                                catch{}

                                bmp.Save(FileNameOutput, info[1], encoderParameters);
                            }
                        }
                    }
                }
            }
        }
    }

}
