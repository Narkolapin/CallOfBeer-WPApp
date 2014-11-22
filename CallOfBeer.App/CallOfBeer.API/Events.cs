using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallOfBeer.API
{
    public class Events
    {
        public Events()
        {

        }

        public string id {get;set;}

        public string name { get; set; }

        public string date { get; set; }

        public Location location { get; set; }

        public Adress adress { get; set; }
    }   

    //Declaration de la structure d'une location
    public struct Location
    {
        public double lon;
        public double lat;
    }

    //Declaration de la structure d'une adress
    public struct Adress
    {
        public string id;
        public string adress;
        public string city;
        public string country;
        public int zip;
    }
    
}
    

