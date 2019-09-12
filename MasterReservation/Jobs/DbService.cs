using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using MasterReservation.Models;
using MasterReservation.Utilities;
using Quartz;

namespace MasterReservation.Jobs
{
    public class DbService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    dbUse.TimeSlotModels.RemoveRange(dbUse.TimeSlotModels.Where(t => t.Date < DateTime.Today));
                    dbUse.BookingModels.RemoveRange(dbUse.BookingModels.Where(t => t.Date < DateTime.Today));
                    dbUse.SaveChanges();
                }
                catch (Exception e)
                {
                    
                }
            }

            using (UserContext dbUse = new UserContext())
            {
                try
                {
                    List<WorkingPlaceModel> allPlaces = Utilities.SendDbUtility.GetAllWorkingPlaces();

                    foreach (var place in allPlaces)
                    {
                        SalonModel salon = Utilities.SendDbUtility.GetSalon(place.SalonId);
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

                                if (!Utilities.SendDbUtility.CheckTimeSlot(dateNow, tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00", place.Id))
                                {
                                    dbUse.TimeSlotModels.Add(new TimeSlotModel()
                                    {
                                        PlaceId = place.Id,
                                        SalonId = place.SalonId,
                                        Time = tempFrom.ToString() + ":00-" + tempTo.ToString() + ":00",
                                        Booked = false,
                                        Date = dateNow
                                    });
                                }
                            }
                            dateNow = dateNow.AddDays(1);
                        }
                    }

                    dbUse.SaveChanges();
                }
                catch (Exception e)
                {

                }
            }


        }
    }
}