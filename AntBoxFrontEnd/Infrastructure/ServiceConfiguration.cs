using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Infrastructure
{
    public class ServiceConfiguration
    {
        private static string _apiKey;
        internal const string SupportedApiVersion = "v1";

        static ServiceConfiguration()
        {
            ApiVersion = SupportedApiVersion;
        }

        internal static string GetApiKey()
        {
            if (string.IsNullOrEmpty(_apiKey))
                _apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["hovakey"];

            return _apiKey;
        }

        public static void SetApiKey(string newApiKey)
        {
            _apiKey = newApiKey;
        }

        public static string ApiVersion { get; internal set; }
    }
}
