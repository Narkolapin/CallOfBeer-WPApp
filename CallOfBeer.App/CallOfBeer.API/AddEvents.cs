using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallOfBeer.API
{
    public class AddEvents
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public String AdressName { get; set; }
        public string Adress { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
