using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Entities;

using System.Net;

namespace AntBoxFrontEnd.Infrastructure
{
    [Serializable]
    public class MissingException : ApplicationException
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public MissingError Error { get; set; }

        public MissingException()
        {
        }

        public MissingException(HttpStatusCode httpStatusCode, MissingError error)
            : base("missing")
        {
            HttpStatusCode = httpStatusCode;
            Error = error;
        }

        public MissingException(HttpStatusCode httpStatusCode, string missing)
            : base("missing")
        {
            HttpStatusCode = httpStatusCode;
            Error = new MissingError { Missing = missing};
        }
    }
}