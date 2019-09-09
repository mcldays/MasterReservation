using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class FavoriteController : Controller
    {
        
        public string SetFavorite(string data)
        {
            try
            {
                int userId = SendDbUtility.GetResidentId(User.Identity.Name);
                bool res = SendDbUtility.SetFavorite(Int32.Parse(data), userId);
                if (!res)
                {
                    return "0";
                }
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
            
        }

        public string RemoveFavorite(string data)
        {
            try
            {
                int userId = SendDbUtility.GetResidentId(User.Identity.Name);
                bool res = SendDbUtility.RemoveFavorite(Int32.Parse(data), userId);
                if (!res)
                {
                    return "0";
                }
                return "1";
            }
            catch (Exception e)
            {
                return "0";
            }
        }
    }
}