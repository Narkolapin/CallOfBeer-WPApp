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
                AddEvents SendEvent = new AddEvents()
                {
                    eventName ="Alors ? 9a va mieu la picole ?",
                    eventDate = "1418824922",
                    addressLat =maPosition.Latitude.ToString().Replace(",","."),
                    addressLon = maPosition.Longitude.ToString().Replace(",", "."),
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