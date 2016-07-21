using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{



    public class abCustomer
    {

        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string LegalName { get; set; }
        public string LegalCode { get; set; }
        public int? Status { get; set; }
        public string ofRecipientId { get; set; }
        public bool? skipSMSNotifications { get; set; }
        public bool? skipPhoneNumberValidation { get; set; }


    }
}