using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Entities;
using System.Text;
using System.Net.Http;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentService : Services
    {


        const int itemPerPage = 10;

        const int Max = 3;
        public int Page { get; set; }

        public PaymentService(string apiKey) : base(apiKey)
        {
        }

        public virtual Boolean CreatePaymentCard(PaymentRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var paymentResponse = Requestor.Post<CreatedCardResponse>(UrlsConstants.PaymentCard, requestOptions, PostData);


            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }


        public virtual Boolean UpdatePayment(PaymentRequestOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var paymentResponse = Requestor.Put<MissingResponse>(UrlsConstants.PaymentCard + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }

        public virtual Boolean DeletePayment(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            try
            {
                Requestor.Delete(UrlsConstants.PaymentCard + "/" + id, requestOptions);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        public virtual List<CardObject> ListPaymetCards(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);



                var payments = Requestor.Get<PaginationListCards>(UrlsConstants.PaymentCard + "/" + id, requestOptions);

                return payments.Cards;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return new List<CardObject>();
            }           
        }


        public virtual PaginationListCards ListPaymetCardsPagination(string id, string paginationId, int page ,RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("items_per_page", Max.ToString());
                parameters.Add("page_number", page.ToString());

                parameters.Add("pagination_id", paginationId);

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var payments = Requestor.Get<PaginationListCards>(UrlsConstants.PaymentCard + "/" + id + encodedParams, requestOptions);

                return payments;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return new PaginationListCards();
            }
        }

        public virtual bool DoCharge(Charge charge, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(charge, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var paymentResponse = Requestor.Post<ChargeResponse>(UrlsConstants.Payment, requestOptions, PostData);


            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        public virtual List<ChargeResponse> ListCharges (string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var charges = Requestor.Get<List<ChargeResponse>>(UrlsConstants.Payment + "/" + id, requestOptions);

                return charges;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }




    }


}