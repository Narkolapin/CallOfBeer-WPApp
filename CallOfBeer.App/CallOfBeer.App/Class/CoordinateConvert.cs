using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Device.Location;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace CallOfBeer.App.Class
{
    public static class CoordinateConvert
    {
        /// <summary>
        /// Convertis les données Geoccordinate en GeoCoordiante
        /// </summary>
        /// <param name="geocoordinate">Element renvoyer par le gps</param>
        /// <returns>Element attendu par la map</returns>
        public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {
            return new GeoCoordinate
                (
                geocoordinate.Latitude,
                geocoordinate.Longitude,
                geocoordinate.Altitude ?? Double.NaN,
                geocoordinate.Accuracy,
                geocoordinate.AltitudeAccuracy ?? Double.NaN,
                geocoordinate.Speed ?? Double.NaN,
                geocoordinate.Heading ?? Double.NaN
                );
        }

        //Affiche la position du joueur
        public static async void MyPosition(MapControl mapHome) 
        {        
            Geolocator maLocation = new Geolocator();
            Geoposition myGeoposition = await maLocation.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate = ConvertGeocoordinate(myGeocoordinate);

            //Creation d'un poit représentant la location de l'utilisateur
            BasicGeoposition newGeo = new BasicGeoposition();
            newGeo.Latitude = myGeoCoordinate.Latitude;
            newGeo.Longitude = myGeoCoordinate.Longitude;

            Geopoint newPoint = new Geopoint(newGeo);

            //Envois sur la map des données
            mapHome.Center = newPoint;
            mapHome.ZoomLevel = 15;
        
            //Ajoute un push pin sur la position de l'utilisateur
            //msdn.microsoft.com/en-us/library/windows/apps/xaml/dn792121.aspx
            MapIcon iconPosition = new MapIcon();
            iconPosition.Location = newPoint;
            iconPosition.Title = "Votre position";
            iconPosition.NormalizedAnchorPoint = new Point(1.0, 1.0);
            //iconPosition.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));
            mapHome.MapElements.Add(iconPosition);

        }

        //Retourne les coordionnées sup gauche, inf droit de la map
        public static void GetMapCornerCoordinate(MapControl maMap, out BasicGeoposition NW, out BasicGeoposition SE)
        {
            NW = new BasicGeoposition();
            SE = new BasicGeoposition();
            Geopoint p = null;

            maMap.GetLocationFromOffset(new Point(0,0),out p);
            NW = p.Position;

            maMap.GetLocationFromOffset(new Point(maMap.ActualWidth, maMap.ActualHeight), out p);
            SE = p.Position;
        }
    }
}
