using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services
{
    public class Services
    {
        public string ApiKey { get; set; }

        protected Services(string apiKey)
        {
            ApiKey = apiKey;
        }

        protected RequestOptions SetupRequestOptions(RequestOptions requestOptions)
        {
            if (requestOptions == null) requestOptions = new RequestOptions();

            if (string.IsNullOrEmpty(requestOptions.ApiKey))
                requestOptions.ApiKey = ApiKey;

            return requestOptions;
        }
    }
}