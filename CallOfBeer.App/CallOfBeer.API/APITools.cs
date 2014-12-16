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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topLat"></param>
        /// <param name="topLon"></param>
        /// <param name="botLat"></param>
        /// <param name="botLon"></param>
        /// <returns></returns>
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
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEvent"></param>
        public async void PostEvent(List<KeyValuePair<string, string>> newEvent)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
           
                string api = "http://api.callofbeer.com/events";
                HttpResponseMessage reponse = await client.PostAsync(api, new FormUrlEncodedContent(newEvent));
                reponse.EnsureSuccessStatusCode();
                if (reponse.IsSuccessStatusCode)
                {
                    string sortie = reponse.Content.ToString();
                    int i = 1;
                }
                else
                {
                    //TODO Gères tes erreurs
                }

                //client.BaseAddress = new Uri("http://api.callofbeer.com/");

            }
        }
    }
}
