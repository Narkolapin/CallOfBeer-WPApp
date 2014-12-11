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

        public async Task<List<Events>> GetEvents(double topLat, double topLon, double botLat, double botLon )
        {
            using(var client = new HttpClient())
            {
                /*client.BaseAddress = new Uri("http://api.callofbeer.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/

                try
                {
                    string str = string.Format("http://api.callofbeer.com/events?topLat={0}&topLon={1}&botLat={2}&botLon={3}", topLat, topLon, botLat, botLon);
                    var response = await client.GetAsync(str);

                    if (response.IsSuccessStatusCode)
                    {
                        List<Events> getEvent = await response.Content.ReadAsAsync<List<Events>>();
                       return getEvent;
                    }

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
