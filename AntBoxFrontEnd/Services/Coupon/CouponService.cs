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

namespace AntBoxFrontEnd.Services.Coupon
{
    public class CouponService : Services
    {
        const int itemPerPage = 100;
        public int Page { get; set; }

        public CouponService(string apiKey) : base(apiKey)
        {
            Page = 1;
        }

        public virtual Boolean CreateCoupon(CouponRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.Coupon, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public virtual Boolean UpdateCoupon(CouponUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.Coupon + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }



            return true;
        }


        public virtual CouponResponse SearchCoupon(string id, RequestOptions requestOptions = null)
        {
            CouponResponse coupon = new CouponResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = UrlHelper.BuildURLParametersString(parameters);

                coupon = Requestor.Get<CouponResponse>(UrlsConstants.Coupon + "/" + id, requestOptions);

            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }

            return coupon;
        }


        public virtual List<CouponResponse> ListCoupon(RequestOptions requestOptions = null)
        {
            PaginationCouponsResponse coupons = new PaginationCouponsResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> ();

                parameters.Add("items_per_page", itemPerPage.ToString());
                parameters.Add("page_number", Page.ToString());

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                coupons = Requestor.Get<PaginationCouponsResponse>(UrlsConstants.CouponList + encodedParams, requestOptions);

            }catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return coupons.Coupons;
        }


        public virtual bool DeleteCoupon(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.Coupon + "/" + id, requestOptions);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }


            return true;
        }


       

    }

}