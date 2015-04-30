using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Diagnostics;
using NearMeSearcher.Resources;
using NearMeSearcher.Model;

namespace NearMeSearcher
{
    /// <summary>
    /// Main page 
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        // Geo location finder
        GeoLocater geoLocater = new GeoLocater(); 

        // Searcher for foursquare
        FourSquareSearcher searcher = new FourSquareSearcher();
        
#region Public functions

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            Venues.ItemsSource = searcher.Venues;
        }

#endregion Public functions

#region Event handlers

        /// <summary>
        /// Event handler for phone application page loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void phoneApplicationPage_OnPageLoaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        ///  Event handler when text input updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTextBox_OnTextInputUpdate(object sender, TextCompositionEventArgs e)
        {
           // No use
        }

        /// <summary>
        /// Event handler for text input started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void searchTextBox_OnTextInputStart(object sender, TextCompositionEventArgs e)
        {
            // No use
        }

        /// <summary>
        /// Event handler for text input finished entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
 
        private void searchTextBox_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            // Safe to search this since this is the final one user wanted
            Debug.WriteLine("SearchText Final : " + SearchTextBox.Text);
            StartSearch(SearchTextBox.Text);
        }

        /// <summary>
        ///  Event handler for search box gaining focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void searchTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Gained focus");

            // Some emulator trail hacks
            // Clear the text when first key in happened
            if (SearchTextBox.Text.Equals(AppResources.SearchDefaultText, StringComparison.CurrentCulture))
            {
                ClearSearchBox();
            }
            else
            {
                // Dont care
            }
        }

        /// <summary>
        /// Event handler for text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine("SearchText entered : " + SearchTextBox.Text);
            StartSearch(SearchTextBox.Text);
        }

#endregion Event handlers

#region Private helper functions
        
        /// <summary>
        ///  Clear the Search box
        /// </summary> 
        private void ClearSearchBox()
        {
           SearchTextBox.Text = String.Empty;
        }

        /// <summary>
        ///  Put all your initialization stuff here
        /// </summary>
        private void Init()
        {
            // Some localization stuff
            this.SearchTextBlock.Text = AppResources.SearchTextBlocText;
            this.SearchTextBox.Text = AppResources.SearchDefaultText;
            // Emulator hack to get keyboard on
            this.SearchTextBox.Focus();
            // Find current location , if no location found go to Greenwich
            geoLocater.Start();
        }

        private void StartSearch(String searchWord)
        {
            String loc =  geoLocater.Location();
            // Many things can go wrong :)))
            if( loc != null && loc != String.Empty && 
                searchWord != null)
                // Search word can be empty
            {
                // Do a search
                searcher.Search(searchWord,loc);
                // some status message           
                resultsTextBlock.Text = "Results for " + searchWord;
            }
            else
            {
                // Show some error message or do nothing
                // May be location not availble yet , so wait for the next search entry
            }
        }
#endregion
    }
}