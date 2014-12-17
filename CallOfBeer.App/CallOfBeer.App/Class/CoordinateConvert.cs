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
using CallOfBeer.API;

namespace CallOfBeer.App.Class
{
    public static class CoordinateConvert
    {
        public static BasicGeoposition topLeft;
        public static BasicGeoposition bottomRight;

        /// <summary>
        /// Charge la map sur la position du joueur
        /// </summary>
        /// <param name="mapHome">la map afficher dans le Xaml</param>
        public static async void MapInit(MapControl mapHome)
        {
            // Coordonées de l'utilisateur
            Geopoint newPoint = new Geopoint(await ActualPosition());

            //Paramétres de la carte
            mapHome.Center = newPoint;
            mapHome.ZoomLevel = 13;

            //Ajoute un push pin sur la position de l'utilisateur
            //msdn.microsoft.com/en-us/library/windows/apps/xaml/dn792121.aspx
            MapIcon iconPosition = new MapIcon();
            iconPosition.Location = newPoint;
            iconPosition.Title = "Votre position";
            iconPosition.NormalizedAnchorPoint = new Point(1.0, 1.0);
            //iconPosition.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));
            mapHome.MapElements.Add(iconPosition);

            GetMapCornerCoordinate(mapHome, out topLeft, out bottomRight);
        }


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


        /// <summary>
        /// Recupére la position de l'appareil
        /// </summary>
        /// <returns>BasicGeoposition avec les données de position de l'utilisateur</returns>
        public static async Task<BasicGeoposition> ActualPosition()
        {
            try
            {
                Geolocator maLocation = new Geolocator();
                maLocation.DesiredAccuracy = PositionAccuracy.High;
                Geoposition myGeoposition = await maLocation.GetGeopositionAsync(maximumAge: TimeSpan.FromSeconds(20), timeout: TimeSpan.FromSeconds(10));
                Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
                GeoCoordinate myGeoCoordinate = ConvertGeocoordinate(myGeocoordinate);

                //Creation d'un poit représentant la location de l'utilisateur
                BasicGeoposition newGeo = new BasicGeoposition();
                newGeo.Latitude = myGeoCoordinate.Latitude;
                newGeo.Longitude = myGeoCoordinate.Longitude;

                return newGeo;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                }
                return new BasicGeoposition() { Latitude = 0, Longitude = 0 };
            }
        }


        /// <summary>
        /// Retourne les coordionnées sup gauche, inf droit de la map
        /// </summary>
        /// <param name="maMap">Map dans la page</param>
        /// <param name="NW">Point sup gauche</param>
        /// <param name="SE">point inf droit</param>
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

        public static void AddMapPoint(MapControl theMap, BasicGeoposition pointToDraw, Events myEvent)
        {

            try
            {
                MapIcon mapicon = new MapIcon();
                mapicon.Location = new Geopoint(
                    new BasicGeoposition()
                    {
                        Latitude = pointToDraw.Latitude,
                        Longitude = pointToDraw.Longitude
                    }
                );
                mapicon.NormalizedAnchorPoint = new Point(0.5, 0.5);
                mapicon.Title = myEvent.name;
                theMap.MapElements.Add(mapicon);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
