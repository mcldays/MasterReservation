using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class CitiesModel
    {
        public IEnumerable<Result> result;
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
    }




}
