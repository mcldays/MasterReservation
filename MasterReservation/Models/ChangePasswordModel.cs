﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class ChangePasswordModel
    {
        public int id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}