using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterReservation.Filters
{
    public class Admin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Utilities.SendDbUtility.IsAdmin(httpContext.User.Identity.Name))
            { 
                return true;
            }

            return false;
        }
    }
}