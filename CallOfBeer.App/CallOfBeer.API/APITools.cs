using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallOfBeer.API
{
    public class APITools
    {

        public async Task<List<Events>> GetEvents()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.callofbeer.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await client.GetStreamAsync("events");

                    /*if (response.IsSuccessStatusCode)
                    {
                        List<Events> getEvent = await response.Content.ReadAsAsync<List<Events>>();
                       return getEvent;
                    }*/

                    return null;
                }
                catch (Exception e)
                {
                    string error = "Une erreur à été levé : " + e.ToString();
                    return null;
                }
            }
        }
    }
}
