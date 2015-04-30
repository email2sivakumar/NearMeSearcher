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
    /// Some four square related constants
    /// </summary>
    public class FourSquareConstants
    {
        // Four square credentials
        public const String clientID = "CYEMKOM4OLTP5PHMOFVUJJAMWT5CH5G1JBCYREATW21XLLSZ";
        public const String clientSecret = "GYNP4URASNYRNRGXR5UEN2TGTKJHXY5FGSAXTIHXEUG1GYM2";

        // Four square urls
        public const String accessTokenUrl = "https://foursquare.com/oauth2/access_token/";
        public const String authorizeUrl = "https://foursquare.com/oauth2/authorize/";
        public const String searchUrl = "https://api.foursquare.com/v2/venues/search/";

        // Api Version
        public const String version = "20130815";
    }
}
