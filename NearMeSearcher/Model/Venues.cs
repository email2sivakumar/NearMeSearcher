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

namespace NearMeSearcher
{
    /// <summary>
    /// Class which holds only the venue data for data binding
    /// </summary> 
    public class Venues
    {      
        
        private String name;
        public String Name
        {
            get
            {
                // Some decoration for empty strings
                if (name != null && name != String.Empty)
                {
                    return name;
                }
                else
                {
                    return "Not Available";
                }
            }
        }
        private String address;
        public String Address
        {
            get
            {
                // Some decoration for empty strings
                if (address != null && address != String.Empty)
                {
                    return address;
                }
                else
                {
                    return "Not Available";
                }
            }
        }

        private int distance;
        public String Distance
        {
            get
            {
                return distance + " miles away";
            }            
        }

        // Simple consturctor
        public Venues(string name, string address, int distance)
        {
            this.name = name;
            this.address = address;
            this.distance = distance;
        }
    }
}
