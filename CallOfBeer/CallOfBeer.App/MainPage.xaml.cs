﻿using CallOfBeer.API;
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
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls.Maps;
using System.Threading.Tasks;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=391641

namespace CallOfBeer.App
{


    public sealed partial class MainPage : Page
    {
        private BasicGeoposition topLeft;
        private BasicGeoposition bottomRight;
        private ObservableCollection<Events> eventListView = new ObservableCollection<Events>();
        private APITools _apiService = new APITools();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            LocationService.LoadMap(MapHome);
        }

        private async void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            //Liste des évènements à retourner
            List<Events> listEventAvailiable = null;

            LocationService.LoadMap(MapHome);
            LocationService.GetMapCornerPosition(MapHome);

            topLeft = LocationService.topLeft;
            bottomRight = LocationService.bottomRight;

            if(MapHome.LoadingStatus != MapLoadingStatus.Loading)
            {
                listEventAvailiable = await this._apiService.GetEvents(topLeft.Latitude, topLeft.Longitude, bottomRight.Latitude, bottomRight.Longitude);

                if (listEventAvailiable.Count != 0)
                {
                    foreach (var item in listEventAvailiable)
                    {
                        //TODO Afficher les elements sur la map

                    }
                }
                else
                {
                    //TODO : afficher qu'il n'y a pas d'events
                }
            }
        }

        private void NewCall(object sender, TappedRoutedEventArgs e)
        { 
            Frame.Navigate(typeof(NewEvent));
        }
    }
}
