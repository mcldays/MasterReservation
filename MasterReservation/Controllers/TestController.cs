using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MasterReservation.Models;
using MasterReservation.Utilities;

namespace MasterReservation.Controllers
{
    public class TestController : Controller
    {
        [AllowAnonymous]
       public async Task<ActionResult> TestMethod(string term)
       {
          List<string> Cities = new List<string>();

         var x = await Utilities.RequestCity.Request(term);
         x.result.Any();
         var res = x.result.Where(t=>t.id!="Free").ToList().Select(t => new
         {
             value = t.name
         });

            return Json(res,JsonRequestBehavior.AllowGet);
        }




        public ActionResult SignUp(string textbox1)
        {
            ViewBag.Test = "ebat_sobak";
            return View("../Home/About");
            //using (UserContext db = new UserContext())
            //{
            //    db.UsersModel.Add(new UserModel()
            //    {
            //        Name = "1234"
            //    });

            //    db.SaveChanges();
            //}
            //return "hyek";

            //poashe


            
        }



    }
}