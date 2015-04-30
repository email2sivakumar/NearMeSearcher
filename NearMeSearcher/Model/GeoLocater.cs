using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using System.Diagnostics;

namespace NearMeSearcher.Model
{
    /// <summary>
    /// Class for finding lat and long
    /// </summary> 
    public class GeoLocater
    {
        // Watcher
        GeoCoordinateWatcher geoWatcher = null;
        
        // Property for location availablity. Will be useful to show something to user.
        // Better then something from GeoCoordinateWatcher
        private bool isLocationAvailable = false;
        public bool IsLocationAvailable
        {
            get
            {
                return isLocationAvailable;
            }
        }

        // Property for latitude
        private double latitute = 0;
        public double Latitute
        {
            get
            {
                return latitute;
            }
        }

        // Property for longtitude
        private double longtitude = 0;
        public double Longtitude
        {
            get
            {
                return longtitude;
            }
        }

        /// <summary>
        /// Util function returns current location if available otherwise greenwich as default ( 0,0)
        /// </summary>
        /// <returns></returns>
        public String Location()
        {
            String loc = null;
            if (isLocationAvailable == true)
            {                
                try
                {
                    loc = String.Format("{0},{1}", Latitute, Longtitude);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Formating exception , " + e.Message);
                }                
            }
            return loc;
        }

        public GeoLocater()
        {
            // Leave all geo watcher properites to default
            geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            // Event handler. Do I need this , may be not :( but it is better than Location property.
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
        }

        /// <summary>
        /// Start the watcher
        /// </summary> 
        public void Start()
        {          
            // Need some user permission to do this stuff. Just check :)
            if (geoWatcher.Status != GeoPositionStatus.Disabled)
            {
                geoWatcher.Start();
            }
            else
            {
                MessageBox.Show("Unable to find your location. Please check your settings");
                Debug.WriteLine("Unable to start geowatcher " + geoWatcher.Status);
            }
        }

        /// <summary>
        /// Stop the watcher
        /// </summary> 
        public void Stop()
        {
            if (geoWatcher != null && geoWatcher.Status == GeoPositionStatus.Ready)
            {
                geoWatcher.Stop();
            }
        }

        /// <summary>
        /// Call back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e != null && e.Position != null && e.Position.Location != null)
            {
                latitute = e.Position.Location.Latitude;
                longtitude = e.Position.Location.Longitude;
                //Debug.WriteLine("Latitude : " + latitute.ToString());
                //Debug.WriteLine("Longtitue : " + longtitude.ToString());
                isLocationAvailable = true;
            }
            else
            {
                Debug.WriteLine("Watcher returned some crapy data , do nothing ");
            }
        }
    }
}
