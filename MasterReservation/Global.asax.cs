﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MasterReservation.App_Start;
using MasterReservation.Jobs;
using MasterReservation.Utilities;

namespace MasterReservation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("ru-RU");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DbSheduler.Start();
        }

        protected void Application_Error(object sender, EventArgs e)

        {

            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

            using (StreamWriter sw = new StreamWriter(logFilePath, true, System.Text.Encoding.UTF8))

            {

                sw.Write(HttpContext.Current.Error);

            }

        }
    }
}
