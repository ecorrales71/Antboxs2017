using AntBoxFrontEnd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Rules
{
    public class RulesService : Services
    {
        public RulesService(string apiKey) : base(apiKey)
        {
        }

        public virtual RulesContentResponse ListRules(RequestOptions requestOptions = null)
        {
            RulesContentResponse rules = new RulesContentResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);
                rules = Requestor.Get<RulesContentResponse>(UrlsConstants.Rules, requestOptions);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return rules;
        }

    }
}