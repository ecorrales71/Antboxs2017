using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Address
{
    public class PaginationAddresses
    {

        public string pagination_id { get; set; }

        public int total { get; set; }

        public int pages { get; set; }

        public List<AddressResponse> ListAddresses { get; set; }

    }
}