using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MasterReservation.Models;
using Newtonsoft.Json;

namespace MasterReservation.Utilities
{
    public static class RequestCity
    {
        public static async Task<CitiesModel> Request(string term)
        {
            HttpClient request =
                new HttpClient();
            return await await request
                .GetAsync("https://kladr-api.ru/api.php?query=" + term + "&contentType=city&withParent=0&limit=5")
                .ContinueWith(async r =>
                {
                    if (r.IsFaulted)
                        return null;
                    CitiesModel x =
                        JsonConvert.DeserializeObject<CitiesModel>(await r.Result.Content.ReadAsStringAsync());
                    return x;
                });



        }
    }
}