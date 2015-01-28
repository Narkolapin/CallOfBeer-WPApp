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
    public static class LocationService
    {
        public static BasicGeoposition topLeft;
        public static BasicGeoposition bottomRight;
        public static Geoposition localPosition;

        /// <summary>
        /// Initialise la map
        /// </summary>
        /// <param name="appMap">nom de la map dans la vue</param>
        public static async void LoadMap(MapControl appMap)
        {
            Geoposition returnedPosition = await GetUserPosition();
            MapIcon userMapIcon = new MapIcon();

            //TODO Initialiser les paramétre de la map
            appMap.ZoomLevel = 13;

            //TODO Definir la position de l'utilisateur
            appMap.Center = returnedPosition.Coordinate.Point;

            //TODO Afficher les élements de la carte
            userMapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));
            userMapIcon.Location = appMap.Center;
            userMapIcon.Title = "Votre position";
            userMapIcon.NormalizedAnchorPoint = new Point(0.5,0.5);
        }

        /// <summary>
        /// Determine la position géographique de l'utilisateur
        /// </summary>
        /// <returns>Géoposition de l'utilisateur</returns>
        private static async Task<Geoposition> UserPosition()
        {
            Geoposition userPosition = null;
            Geolocator gpsValuePosition = new Geolocator();

            try
            {
                userPosition = await gpsValuePosition.GetGeopositionAsync();
                return userPosition;
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreure est survenue : "+ ex.Message);
            }
        }


        public static async Task<Geoposition> GetUserPosition()
        {
            Geoposition userPosition = await UserPosition();
            return userPosition;
        }


        /// <summary>
        /// Retourne les coordonnées des coté de la map
        /// </summary>
        /// <param name="mapControl"></param>
        public static void GetMapCornerPosition(MapControl mapControl)
        {
            Geopoint geoP;
            mapControl.GetLocationFromOffset(new Point(0, 0), out geoP);
            LocationService.topLeft = geoP.Position;

            mapControl.GetLocationFromOffset(new Point(mapControl.ActualWidth, mapControl.ActualHeight), out geoP);
            LocationService.bottomRight = geoP.Position;
        }

        /// <summary>
        /// Charge la map sur la position du joueur
        /// </summary>
        /// <param name="mapHome">la map afficher dans le Xaml</param>
       /* public static async void MapInit(MapControl mapHome)
        {
            //Paramétres de la carte
            mapHome.Center = newPoint;
            mapHome.ZoomLevel = 13;

            // Coordonées de l'utilisateur
            Geopoint newPoint = new Geopoint(await ActualPosition());

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
                //Position Géographique actuel
                Geolocator maLocation = new Geolocator() { 
                    DesiredAccuracy = PositionAccuracy.High 
                };

                if (maLocation.LocationStatus != PositionStatus.Initializing && maLocation.LocationStatus != PositionStatus.NotInitialized)
                    throw new Exception("Un erreure est survenue lors de l'initialisation de la géolocalisation");

                //Emplacement d'un utilisateur 
                Geoposition myGeoposition = await maLocation.GetGeopositionAsync();

                if (myGeoposition.Coordinate == null)
                    throw new Exception("Valeurs du GPS null");

                Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
                GeoCoordinate myGeoCoordinate = ConvertGeocoordinate(myGeocoordinate);


                //Creation d'un point représentant la location de l'utilisateur
                BasicGeoposition userPointPosition = new BasicGeoposition() { 
                    Latitude = myGeoCoordinate.Latitude, 
                    Longitude = myGeoCoordinate.Longitude 
                };

                return userPointPosition;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                }
                return new BasicGeoposition()
                {
                    Longitude = 0.0,
                    Latitude = 0.0
               
                };
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

        }*/
    }
}
