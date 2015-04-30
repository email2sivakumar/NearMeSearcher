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
using System.Collections.Generic;

namespace NearMeSearcher
{
    /// <summary>
    /// Class which holds the structure of json response from Foursquare
    /// </summary> 
    public class SearchResponse
    {
        public class Meta
        {
            public int code { get; set; }
        }

        public class Contact
        {
            public string phone { get; set; }
            public string formattedPhone { get; set; }
        }

        public class Location
        {
            public string address { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public int distance { get; set; }
            public string postalCode { get; set; }
            public string cc { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string crossStreet { get; set; }
        }

        public class Stats
        {
            public int checkinsCount { get; set; }
            public int usersCount { get; set; }
            public int tipCount { get; set; }
        }

        public class Specials
        {
            public int count { get; set; }
            public List<object> items { get; set; }
        }

        public class HereNow
        {
            public int count { get; set; }
            public List<object> groups { get; set; }
        }

        public class Venue
        {
            public string id { get; set; }
            public string name { get; set; }
            public Contact contact { get; set; }
            public Location location { get; set; }
            public List<object> categories { get; set; }
            public bool verified { get; set; }
            public bool restricted { get; set; }
            public Stats stats { get; set; }
            public Specials specials { get; set; }
            public HereNow hereNow { get; set; }
            public string referralId { get; set; }
        }

        public class Response
        {
            public List<Venue> venues { get; set; }
        }

        public class Root
        {
            public Meta meta { get; set; }
            public Response response { get; set; }
        }
    }
}
