using CallOfBeer.API;
using CallOfBeer.App.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private void Launch_Event(object sender, TappedRoutedEventArgs e)
        {
            BasicGeoposition maPosition = new BasicGeoposition(){
            Latitude = CoordinateConvert.ActualPosition().Result.Latitude,
            Longitude = CoordinateConvert.ActualPosition().Result.Longitude
            };

            AddEvents newEvent = new AddEvents()
            {
               Name = this.event_adress.Text,
               AdressName = event_adressname.Text,
               Adress = event_adress.Text,
               Zip = Convert.ToInt32(event_zip),
               City = event_city.Text,
               Country = event_country.Text,
               Date = DateTime.Now,
               Lat = maPosition.Latitude,
               Long = maPosition.Longitude
            };

            APITools api = new APITools();
            api.PostEvent(newEvent);


        }
    }
}
