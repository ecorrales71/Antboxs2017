using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentDetailResponseParent : Response
    {

        [JsonProperty("payments")]
        public List<PaymentDetailResponse> Payments { get; set; }

    }
}