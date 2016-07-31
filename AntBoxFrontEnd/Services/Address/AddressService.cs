using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Entities;
using System.Text;
using AntBoxFrontEnd.Models;


using System.Threading.Tasks;

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressService : Services
    {
        public AddressService (string apiKey = null) : base(apiKey)
        {

        }


        //public virtual Boolean CreateAddress(AddressRequestOptions createOptions, RequestOptions requestOptions = null)
        //{
        //    requestOptions = SetupRequestOptions(requestOptions);


        //    requestOptions = SetupRequestOptions(requestOptions);

        //    string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
        //    var PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");
        //    var destination = Requestor.Post<>(UrlsConstants.Customer, requestOptions, PostData);


        //    return true;
        //}



    }
}