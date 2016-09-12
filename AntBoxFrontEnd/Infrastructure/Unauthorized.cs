using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AntBoxFrontEnd.Infrastructure
{
    [Serializable]
    public class UnauthorizedException: ApplicationException
    {

        public string Error { get; set; }

        public UnauthorizedException()
        {
            Error = "ERROR DE AUTENTICAZIÓN";
        }
    }


}