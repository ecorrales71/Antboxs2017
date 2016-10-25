using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class CouponModel
    {
        
        public PaginationCouponsResponse Coupons { get; set; }

        public PaginationCodesResponse Codes { get; set; }

        public List<UserResponse> Users { get; set; }

    }
}