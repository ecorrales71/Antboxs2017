using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Infrastructure;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipcodeService : Services
    {
        public ZipcodeService(string apiKey) : base(apiKey)
        {
        }

        public List<ZipCodeResponse> SearchZipCode (string zipcode, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "code", zipcode} };

            var encodedContent = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var zipResponse = Requestor.Get<List<ZipCodeResponse>>(UrlsConstants.ZipCode + encodedContent, requestOptions);

            return zipResponse;
        }

    }
}