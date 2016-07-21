using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class abTask
    {

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> OrderId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> RealDate { get; set; }
        public string OnFleetToken { get; set; }
        public Nullable<bool> PickUpTask { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> EstimatedDateAfter { get; set; }
        public Nullable<System.DateTime> EstimatedDateBefore { get; set; }
        public Nullable<System.Guid> ADDRESSID { get; set; }
    }
}