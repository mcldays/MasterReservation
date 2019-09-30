using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using MasterReservation.Models;
using MasterReservation.Utilities;
using Quartz;

namespace MasterReservation.Jobs
{
    public class DbService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                string isError = "";
                int addedSlots = 0;
                int deletedSlots = 0;
                using (UserContext dbUse = new UserContext())
                {
                    try
                    {
                        IEnumerable<TimeSlotModel> slots = dbUse.TimeSlotModels.Where(t => t.Date < DateTime.Today);
                        dbUse.TimeSlotModels.RemoveRange(slots);
                        dbUse.BookingModels.RemoveRange(dbUse.BookingModels.Where(t => t.Date < DateTime.Today));
                        var lusd = slots.ToList();
                        deletedSlots = lusd.Count();
                        dbUse.SaveChanges();
                        
                    }
                    catch (Exception e)
                    {
                        isError = "Ошибка в удалении прошедших дат:\n" + e.ToString();
                    }
                }

                using (UserContext dbUse = new UserContext())
                {
                    try
                    {
                        List<WorkingPlaceModel> allPlaces = SendDbUtility.GetAllWorkingPlaces();

                        foreach (var place in allPlaces)
                        {
                            //SalonModel salon = SendDbUtility.GetSalon(place.SalonId);
                            //string[] workingTime = salon.OperatingMode.Split('-');
                            //int constFrom = Int32.Parse(workingTime[0].Trim().Substring(0, 2));
                            //int constTo = Int32.Parse(workingTime[1].Trim().Substring(0, 2));

                            //int from;
                            //int to;
                            //int tempTo;
                            //int tempFrom;
                            //DateTime dateNow = DateTime.Today;

                            //for (int i = 0; i < 14; i++)
                            //{

                            //    from = constFrom;
                            //    to = constTo;
                            //    while (from != to)
                            //    {
                            //        tempFrom = from;
                            //        tempTo = ++from;
                            //        string isExist = SendDbUtility.CheckTimeSlot(dateNow,
                            //            tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00", place.Id);
                            //        if (isExist == "0")
                            //        {
                            //            dbUse.TimeSlotModels.Add(new TimeSlotModel()
                            //            {
                            //                PlaceId = place.Id,
                            //                SalonId = place.SalonId,
                            //                Time = tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00",
                            //                Booked = false,
                            //                Date = dateNow
                            //            });
                            //            addedSlots++;
                            //        }
                            //        else if(isExist != "1")
                            //        {
                            //            isError = "Ошибка в проверке тайм слота:\n" + isExist;
                            //        }
                            //    }
                            //    dateNow = dateNow.AddDays(1);
                            //}



                            SalonModel salon = SendDbUtility.GetSalon(place.SalonId);
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
                            int constFromSat = Int32.Parse(workingTimeSat[0].Trim().Substring(0, 2));
                            int constToSat = Int32.Parse(workingTimeSat[1].Trim().Substring(0, 2));

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
                                else if(dateNow.DayOfWeek == DayOfWeek.Monday)
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

                                    string isExist = SendDbUtility.CheckTimeSlot(dateNow,
                                                    tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00", place.Id);
                                    if (isExist == "0")
                                    {

                                        dbUse.TimeSlotModels.Add(new TimeSlotModel()
                                        {
                                            PlaceId = place.Id,
                                            SalonId = place.SalonId,
                                            Time = tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00",
                                            Booked = false,
                                            Date = dateNow
                                        });
                                        addedSlots++;
                                    }
                                    else if (isExist != "1")
                                    {
                                        isError = "Ошибка в проверке тайм слота:\n" + isExist;
                                    }
                                }
                                
                                dateNow = dateNow.AddDays(1);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        isError = "Ошибка в добавлении новых дат:\n" + e.ToString();
                    }
                    if (isError != "")
                    {
                        WriteLogs(isError);
                        await Execute(context);
                    }
                    else
                    {
                        dbUse.SaveChanges();
                        WriteLogs("Обновление прошло успешно \nУдалено " + deletedSlots + " слотов \nДобавлено " + addedSlots + " слотов");
                    }
                }
            }
            catch (Exception e)
            {
                WriteLogs("Глобальная ошибка обновления");
            }
        }

        public void WriteLogs(string msg)
        {
            using (StreamWriter sw = new StreamWriter(HostingEnvironment.ApplicationHost.GetPhysicalPath() + @"\file.txt", true, System.Text.Encoding.Default))
            //using (StreamWriter sw = new StreamWriter(@"C:\Users\lexle\Desktop\WPFProjects\MasterReservation\MasterReservation\file.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString() + " " + msg + "\n\n\n");
            }
        }

        public async Task SendMessage(string msg)
        {
            using (MailMessage message = new MailMessage("db-notify@yandex.ru", "lexlex9797@yandex.ru"))
            {
                message.Subject = "Отчет об обновлении бд";
                message.Body = msg;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.yandex.ru",
                    Port = 25,
                    Credentials = new NetworkCredential("db-notify@yandex.ru", "Qq4chaN")
                })
                {
                    await client.SendMailAsync(message);
                }
            }
        }
    }
}