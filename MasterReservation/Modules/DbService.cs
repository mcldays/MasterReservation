using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Modules
{
    public class DbService : IHttpModule
    {
        static Timer timer;
        long interval = 30000; //30 секунд
        static object synclock = new object();
        static bool flag = false;
        int hours = 22;
        int minutes = 00;

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(Service), null, 0, interval);
        }

        private void Service(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if (dd.Hour == hours && dd.Minute == minutes && flag == false)
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
                                dbUse.SaveChanges();
                                deletedSlots = slots.Count();
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
                                    SalonModel salon = SendDbUtility.GetSalon(place.SalonId);
                                    string[] workingTime = salon.OperatingMode.Split('-');
                                    int constFrom = Int32.Parse(workingTime[0].Trim().Substring(0, 2));
                                    int constTo = Int32.Parse(workingTime[1].Trim().Substring(0, 2));

                                    int from;
                                    int to;
                                    int tempTo;
                                    int tempFrom;
                                    DateTime dateNow = DateTime.Today;

                                    for (int i = 0; i < 14; i++)
                                    {

                                        from = constFrom;
                                        to = constTo;
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
                                                isError = "Ошибка в проверке тайм слота:"/* + isExist*/;
                                            }
                                        }
                                        dateNow = dateNow.AddDays(1);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                isError = "Ошибка в добавлении новых дат:"/* + e.ToString()*/;
                            }
                            if (isError != "")
                            {
                                WriteLogs(isError);
                            }
                            else
                            {
                                dbUse.SaveChanges();
                                WriteLogs("Обновление прошло успешно\nУдалено " + deletedSlots + " слотов\nДобавлено " + addedSlots + " слотов");
                                flag = true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        WriteLogs("Глобальная ошибка обновления");
                    }

                }
                else if (dd.Hour != hours && dd.Minute != minutes)
                {
                    flag = false;
                }
            }
        }


        public void WriteLogs(string msg)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\MasterReservation\file.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString() + " " + msg + "\n\n\n");
            }
        }


        public void SendMessage(string msg)
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
                    client.Send(message);
                }
            }
        }


        public void Dispose()
        { }
    }
}