using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Entities;

using System.Net;

namespace AntBoxFrontEnd.Infrastructure
{
    [Serializable]
    public class ServiceException : ApplicationException
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ServiceError Error { get; set; }

        public ServiceException()
        {
        }

        public ServiceException(HttpStatusCode httpStatusCode, ServiceError error, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Error = error;
        }

        public ServiceException(HttpStatusCode httpStatusCode, ServiceError ServiceError)
            : base(ServiceError.Message.Message)
        {
            HttpStatusCode = httpStatusCode;
            Error = ServiceError;
        }
    }
}