using CallOfBeer.API;
using CallOfBeer.App.Class;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace CallOfBeer.App
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class NewEvent : Page
    {
        public NewEvent()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Taped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void Launch_Event(object sender, TappedRoutedEventArgs e)
        {
            BasicGeoposition maPosition = new BasicGeoposition();
            //maPosition = CoordinateConvert.ActualPosition().Result;
            
            Geolocator maLocation = new Geolocator();
            Geoposition myGeoposition = await maLocation.GetGeopositionAsync(maximumAge: TimeSpan.FromSeconds(20), timeout: TimeSpan.FromSeconds(10));
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate = CoordinateConvert.ConvertGeocoordinate(myGeocoordinate);

            maPosition.Longitude = myGeocoordinate.Longitude;
            maPosition.Latitude = myGeocoordinate.Latitude;

            try
            {
                var tosend = new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>("Key", null),
                    new KeyValuePair<string, string>("eventName", "Alors ? 9a va mieu la picole ?"),
                    new KeyValuePair<string, string>("eventDate", "42"),
                    new KeyValuePair<string, string>("addressLat", maPosition.Longitude.ToString().Replace(",",".")),
                    new KeyValuePair<string, string>("addressLon", maPosition.Latitude.ToString().Replace(",",".")),
                    new KeyValuePair<string, string>("addressName", "verre plein, je te vide"),
                    new KeyValuePair<string, string>("addressAddress", "Verre vide je te plains"),
                    new KeyValuePair<string, string>("addressZip", "66000"),
                    new KeyValuePair<string, string>("addressCity", "MériRpz"),
                    new KeyValuePair<string, string>("addressCountry", "Fr"),
                };

                AddEvents SendEvent = new AddEvents()
                {
                    eventName ="Alors ? 9a va mieu la picole ?",
                    eventDate ="42",
                    addressLat =maPosition.Longitude.ToString().Replace(",","."),
                    addressLon =maPosition.Latitude.ToString().Replace(",","."),
                    addressName ="verre plein, je te vide",
                    addressAddress ="Verre vide je te plains",
                    addressZip ="66000",
                    addressCity ="MériRpz",
                    addressCountry ="Fr"
                };


                APITools api = new APITools();
                api.PostEvent(SendEvent);
            }
            catch (Exception fe) { }



        }
    }
}


/*Class1 tosend = new Class1()
{

    adresss = new Adresss
    {
        Address = "zlefnzelf", 
        City = "lzfjzekcné",
        Country="qmfj",
        Geolocalisation = new double[2]{ maPosition.Latitude,maPosition.Longitude},
        Name = "lsnz"
                
    },
    date =  new TimeSpan(4,3,2),
    name = "PUTAIN TU VAS MARCHER"
                          
};*/
/*AddEvents newEvent = new AddEvents()
{
    Name = this.event_adress.Text,
    AdressName = event_adressname.Text,
    Adress = 8,
    Zip = 33000,
    City = event_city.Text,
    Country = event_country.Text,
    Date = new TimeSpan(4,3,2),
    Geolocalisation = new double[2]{ maPosition.Latitude,maPosition.Longitude}

};*/

/* var toSend = new FormUrlEncodedContent(new[]
{
//eventName, eventDate, addressLon, addressLat. To Update : eventId. Options : addressName, addressAddress, addressZip, addressCity, addressCountry
new KeyValuePair<string, string>("eventName", newEvent.Name),
new KeyValuePair<string, string>("eventDate", newEvent.Date.ToString()),
new KeyValuePair<string, string>("addressLat", newEvent.Lat.ToString()),
new KeyValuePair<string, string>("addressLon", newEvent.Long.ToString())
                
/*new KeyValuePair<string, string>("eventName", newEvent.Name),
new KeyValuePair<string, string>("eventDate", newEvent.Date.ToString()),
new KeyValuePair<string, string>("addressLat", newEvent.Lat.ToString()),
new KeyValuePair<string, string>("addressLon", newEvent.Long.ToString()),
new KeyValuePair<string, string>("addressName", newEvent.AdressName),
new KeyValuePair<string, string>("addressAddress", newEvent.Adress),
new KeyValuePair<string, string>("addressZip", newEvent.Zip.ToString()),
new KeyValuePair<string, string>("addressCity", newEvent.City),
new KeyValuePair<string, string>("addressCountry", newEvent.Country),
});*/