﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterReservation.Models
{
    public class ResidentModel
    {
        [Key]
        public int Id { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public string ServicesIds { get; set; }
        public string StudyPlace { get; set; }
        public string Experience { get; set; }
        public string Awards { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string Offers { get; set; }

        public bool IsAdmin { get; set; }
        public string Favorites { get; set; }
    }
}