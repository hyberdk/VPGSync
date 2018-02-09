using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGSync
{
    internal class Constants
    {
        /// <summary>
        /// Groupname/Label in Google contacts.
        /// </summary>
        public const string GroupName = "VPGSync";

        /// <summary>
        /// Google Client secret as from "Credentials" in https://console.developers.google.com/apis/
        /// Note this is secret, you need to add your own!
        /// See: https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth
        /// </summary>
        public const string GoogleClientSecret = "";

        /// <summary>
        /// Google Client Id as from "Credentials" in https://console.developers.google.com/apis/
        /// Note this is secret, you need to add your own!
        /// See: https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth
        /// </summary>
        public const string GoogleClientId = "";

        /// <summary>
        /// Connection string to connect to database server, you need to provide your own!
        /// </summary>
        public const string VPDBConnectionString = "";
    }
}
