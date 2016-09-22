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

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class AntBoxesServices : Services
    {
        const int itemPerPage = 40;
        public int Page { get; set; }

        public AntBoxesServices(string apiKey) : base(apiKey)
        {
        }
        



        public virtual string CreateAntBoxes(AntBoxRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var antBoxResponse = Requestor.Post<AntBoxResponse>(UrlsConstants.AntBoxOut, requestOptions, PostData);
                return antBoxResponse.Folio;
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return String.Empty;
            }            
        }

        public virtual Boolean UpdateAntBox(AntBoxUpdateRequest createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");



            var customerResponse = Requestor.Put<AntBoxResponse>(UrlsConstants.AntBox + "/" + id, requestOptions, PostData);

            if (string.IsNullOrEmpty(customerResponse.Folio))
                return true;

            return false;

        }


        public virtual PaginationAntBoxes ListAntBoxes(string id, AntBoxStatusEnum status, int currentPage, string idPagination = null,  RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();                

                parameters.Add("items_per_page", itemPerPage.ToString());

                var e = AntBoxStatusEnum.Stored;                
                
                if (!string.IsNullOrEmpty(idPagination))
                {
                    parameters.Add("pagination_id", idPagination);
                    parameters.Add("page_number", currentPage.ToString());
                } 

                if(status.ToString() != AntBoxStatusEnum.Defualt.ToString())
                    parameters.Add("status", status.ToString());

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var antBoxes = Requestor.Get<PaginationAntBoxes>(UrlsConstants.AntBoxList + "/" + id + encodedParams, requestOptions);

                return antBoxes;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }
        
    }


    public struct AntBoxStatusEnum
    {
        private int value;
        private string name;

        private AntBoxStatusEnum(int value, string name)
        {
            this.value = value;
            this.name = name;
        }


        public static readonly AntBoxStatusEnum Reserved = new AntBoxStatusEnum (0, "reserved");
        public static readonly AntBoxStatusEnum Delivering = new AntBoxStatusEnum(0, "delivering");
        public static readonly AntBoxStatusEnum Stored = new AntBoxStatusEnum(0, "stored");
        public static readonly AntBoxStatusEnum Free = new AntBoxStatusEnum(0, "free");
        public static readonly AntBoxStatusEnum Defualt = new AntBoxStatusEnum(0, "Defualt");

        public override string ToString()
        {
            return this.name;
        }
    }


}