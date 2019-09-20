using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class PhotoModel
    {
        public string FileName { get; set; }
        public IEnumerable<byte> SalonPictures { get; set; }


    }
}