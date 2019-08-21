using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MasterReservation.Models;
using MasterReservation.Utilities;


namespace MasterReservation.Controllers
{
    public class DbController : ApiController
    {
        public bool AddNewUserResident([FromUri]string Surname, [FromUri]string Name, [FromUri]string Patronomyc, [FromUri]
            string Phone, [FromUri]string Email, [FromUri]string ServicesIds, [FromUri]string StudyPlace, [FromUri]string Experience, [FromUri]
            string Awards)
        {
            try
            {


                using (UserContext DbUse = new UserContext())
                {
                    DbUse.ResidentModels.Add(new ResidentModel()
                    {
                       Surname = Surname,
                       Name = Name,
                       Patronymic =  Patronomyc,
                       Phone = Phone,
                       Email = Email,
                       ServicesIds = ServicesIds,
                       StudyPlace = StudyPlace,
                       Awards = Awards,
                       Experience = Experience


                    });

                    
                    DbUse.SaveChanges();
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
            }


            

        }

        public bool AddNewUserSalon([FromUri]string ContactPerson, [FromUri]string Phone, [FromUri]string City, [FromUri]
            string Email, [FromUri]string Message)
        {
            try
            {


                using (UserContext DbUse = new UserContext())
                {
                    DbUse.SalonModels.Add(new SalonModel()
                    {
                        ContactPerson = ContactPerson,
                        Phone = Phone,
                        City = City,
                        Email = Email,
                        Message = Message



                    }) ;


                    DbUse.SaveChanges();
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
            }




        }

                                    
        [System.Web.Mvc.HttpGet]
        public bool DeleteUserResident([FromUri] int id)
        {
            try
            {


                using (UserContext dbUse = new UserContext())
                {
                    ResidentModel user = dbUse.ResidentModels.FirstOrDefault(t => t.Id == id);

                    dbUse.ResidentModels.Remove(user);

                    dbUse.SaveChanges();
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
            }



        }

        public bool DeleteUserSalon([FromUri] int id)
        {
            try
            {


                using (UserContext dbUse = new UserContext())
                {
                    SalonModel user = dbUse.SalonModels.FirstOrDefault(t => t.Id == id);

                    dbUse.SalonModels.Remove(user);

                    dbUse.SaveChanges();
                }

                return true;

            }
            catch (Exception e)
            {
                return false;
            }



        }




        [System.Web.Mvc.HttpGet]
        public bool ChangeResident([FromUri]string Surname, [FromUri]string Name, [FromUri]string Patronomyc, [FromUri]
            string Phone, [FromUri]string Email, [FromUri]string ServicesIds, [FromUri]string StudyPlace, [FromUri]string Experience, [FromUri]
            string Awards)
        {
            using (UserContext dbUse = new UserContext())
            {

               

                try
                {


                    using (UserContext DbUse = new UserContext())
                    {
                        DbUse.ResidentModels.AddOrUpdate(new ResidentModel()
                        {
                            Surname = Surname,
                            Name = Name,
                            Patronymic = Patronomyc,
                            Phone = Phone,
                            Email = Email,
                            ServicesIds = ServicesIds,
                            StudyPlace = StudyPlace,
                            Awards = Awards,
                            Experience = Experience


                        });


                        DbUse.SaveChanges();
                    }

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }


            } 
        
                // dbUse.SaveChanges();
            }


       




    }

    }

