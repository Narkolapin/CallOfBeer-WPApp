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
            using (var client = new HttpClient())
            {
                 client.BaseAddress = new Uri("http://api.callofbeer.com/");
                 client.DefaultRequestHeaders.Accept.Clear();
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    string url = string.Format("http://api.callofbeer.com/events?topLat={0}&topLon={1}&botLat={2}&botLon={3}", topLat, topLon, botLat, botLon).Replace(",",".");

                    HttpResponseMessage reponse = await client.GetAsync(new Uri(url));
                   reponse.EnsureSuccessStatusCode();

                    if (reponse.IsSuccessStatusCode)
                    {
                        List<Events> getEvent = await reponse.Content.ReadAsAsync<List<Events>>();
                        return getEvent;
                    }
                }
                catch (HttpRequestException e)
                {
                    string error = "Une erreur à été levé : " + e.ToString();
                    return null;
                }

                return null;
            }


   
                //Test
                /* var reponse = new HttpResponseMessage();
                HttpClient client = new HttpClient();
                string url = string.Format("http://api.callofbeer.com/events?topLat={0}&topLon={1}&botLat={2}&botLon={3}", topLat, topLon, botLat, botLon);
                
                try{
                reponse = await client.GetAsync(url);
                reponse.EnsureSuccessStatusCode();
                List<Events> getEvent = await reponse.Content.ReadAsAsync<List<Events>>();
                

                }
                catch(Exception e){

                }*/
        }


        public async void PostEvent(AddEvents newEvent)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.callofbeer.com/");
                HttpResponseMessage reponse = new HttpResponseMessage();

                reponse = await client.PostAsJsonAsync("http://api.callofbeer.com/", newEvent);
                if (reponse.IsSuccessStatusCode)
                {

                }
            }
        }
    }
}
