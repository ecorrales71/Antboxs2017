using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Services.Customer;

namespace AntBoxFrontEnd.Services.Login
{
    public class UserManager
    {
        /// <summary>
        /// Valida un usuario y devuelve el id del usuario 
        /// </summary>
        /// <param name="usr"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public bool IsValid(string mail, string psw, ref string id)
        {
            try
            {
                var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
                {
                    Email = mail,
                    Password = psw
                };

                LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

                id = ls.HovaLogin(usr);

            }
            catch(Exception ex)
            {

                return false;
            }

            return true;
        }

        public CustomerResponse GetCustomer(string idCustomer)
        {

            CustomerResponse customer = null;
            try
            {
                var service = new CustomerServices(ServiceConfiguration.GetApiKey());

                customer = service.SearchCustomer(idCustomer);


            }
            catch(Exception ex)
            {

                return null;
            }


            return customer;

        }



    }
}