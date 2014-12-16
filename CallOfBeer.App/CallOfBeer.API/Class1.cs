using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallOfBeer.API
{
    public class Class1
    {
        public string id {get;set;}

        public string name { get; set; }

        public TimeSpan date { get; set; }

        public Adresss adresss { get; set; }
    }   

        //Declaration de la structure d'une adress
        public struct Adresss
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public double[] Geolocalisation { get; set; }
        }
    
}
