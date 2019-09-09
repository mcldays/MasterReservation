using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class PictureModel
    {
        public int id { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }


    }
}