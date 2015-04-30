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
using System.Diagnostics;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Threading;

namespace NearMeSearcher
{
    /// <summary>
    /// Class for web query and response
    /// </summary> 
    public class FourSquareSearcher
    {
        // Web client
        WebClient webClient = null;

        //Just because of asyn operations store this
        String uri = String.Empty;

        // Result storage
        private ObservableCollection<Venues> venues = new ObservableCollection<Venues>();
        public ObservableCollection<Venues> Venues
        {
            get
            {
                return venues;
            }
        }

        /// <summary>
        /// Just for testing
        /// </summary>
        public void TestInit()
        {
            venues.Add(new Venues("testName", "testAddress", 100));
            venues.Add(new Venues("testName", "testAddress", 100));
            venues.Add(new Venues("testName", "testAddress", 100));
        }

        /// <summary>
        /// Costructor
        /// </summary> 
        public FourSquareSearcher()
        {
            webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_OnDownloadCompleated;
            //TestInit();
        }
        
        /// <summary>
        /// Search for word and location
        /// </summary>
        /// <param name="searchWord"></param>
        /// <param name="location"></param>
        /// <returns></returns>
 
        public void Search(String searchWord, String location)
        {            
            // new uri for request
            uri = Format(searchWord, location);
            if (webClient.IsBusy)
            {
                // Cancel and new uri request will begin upon cancellation.
                Cancel();
            }
            else
            {
                // No search ongoing. Send some request.
                SendRequest();
            }
        }

        /// <summary>
        /// Cancel the web operation
        /// </summary> 
        public void Cancel()
        {
            try
            {
                Debug.WriteLine("Cancelling the web operation");
                webClient.CancelAsync();
            }
            catch (Exception e)
            {
                // Do nothing and wait for next search..
                Debug.WriteLine("Cancellation failed, " + e.Message);
            }
        }

        /// <summary>
        /// Event handler for web requests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void webClient_OnDownloadCompleated(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled != true)
            {
                Debug.WriteLine("Response : " + e.Result);
                NearMeSearcher.SearchResponse.Root objects = JsonConvert.DeserializeObject<NearMeSearcher.SearchResponse.Root>(e.Result);
                //Remove previous results
                venues.Clear();
                // Add new ones
                foreach (var venue in objects.response.venues)
                {
                    venues.Add(new Venues(venue.name, venue.location.address, venue.location.distance));
                }
            }
            else
            {
                Debug.WriteLine("Download cancelled. No worries");
                // Great may be new search arrived , so make a request from uri
                SendRequest();
            }
        }

        /// <summary>
        ///  Util for formating
        /// </summary>
        /// <param name="searchWord"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private String Format(String searchWord, String location)
        {
            String uri = null;
            try
            {
                uri = String.Format(FourSquareConstants.searchUrl +
                                    "?" +
                                    "client_id={0}&client_secret={1}&v={2}&ll={3}&query={4}",
                                    FourSquareConstants.clientID,
                                    FourSquareConstants.clientSecret,
                                    FourSquareConstants.version,
                                    location,
                                    searchWord);
                Debug.WriteLine("Search uri : " + uri);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Format exception : " + e.Message);
                // Do nothing.. null uri should be handled by callee
            }
            return uri;
        }

        /// <summary>
        /// Send a async http get request
        /// </summary>
        /// <returns></returns>
        private void SendRequest()
        {
            if (uri != null || uri != String.Empty)
            {
                try
                {
                    webClient.DownloadStringAsync(new Uri(uri));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    // Do nothing and wait for the next request....
                }
            }
            else
            {
                Debug.WriteLine("Invalid Uri ");
                // Do nothing and wait for the next request....
            }
        }
    }
}
