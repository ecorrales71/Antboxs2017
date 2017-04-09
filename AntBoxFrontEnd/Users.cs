namespace AntBoxFrontEnd
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public int id { get; set; }

        [StringLength(250)]
        public string email { get; set; }

        public string customerid { get; set; }

        public string token { get; set; }

        public bool? status { get; set; }

        public short? step { get; set; }
    }
}
