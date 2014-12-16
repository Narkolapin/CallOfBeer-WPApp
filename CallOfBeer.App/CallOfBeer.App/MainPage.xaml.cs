using CallOfBeer.API;
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
using CallOfBeer.App.Class;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace CallOfBeer.App
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d’événement décrivant la manière dont l’utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: préparer la page pour affichage ici.

            // TODO: si votre application comporte plusieurs pages, assurez-vous que vous
            // gérez le bouton Retour physique en vous inscrivant à l’événement
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Si vous utilisez le NavigationHelper fourni par certains modèles,
            // cet événement est géré automatiquement.
        }

        private async void MainPageLoaded(object sender, RoutedEventArgs e)
        {

            CoordinateConvert.MapInit(MapHome);
            BasicGeoposition topLeft = CoordinateConvert.topLeft;
            BasicGeoposition bottomRight = CoordinateConvert.bottomRight;
            
            APITools connectAPI = new APITools();
            List<Events> events = await connectAPI.GetEvents(topLeft.Latitude, topLeft.Longitude, bottomRight.Latitude, bottomRight.Longitude);

        }

        private void NewCall(object sender, TappedRoutedEventArgs e)
        {
            BasicGeoposition topLeft;
            BasicGeoposition bottomRight;
            CoordinateConvert.GetMapCornerCoordinate(MapHome, out topLeft, out bottomRight);
            List<BasicGeoposition> maListe = new List<BasicGeoposition>(){
                topLeft,
                bottomRight
            };
            Frame.Navigate(typeof(NewEvent), maListe);
        }
    }
}
