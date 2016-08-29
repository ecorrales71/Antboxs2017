using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AntBoxFrontEnd.Entities
{
    public class AntBox
    {

        public string Model { get; set; }

        public string Description { get; set; }

        public string Sizes { get; set; }

        public decimal Price { get; set; }
        
        public decimal Secure { get; set; }

        public string Label { get; set; }



        public AntBox GetAntBox(AntBoxTypeEnum type)
        {
            var antBox = new AntBox();

            var service = new BoxesService(ServiceConfiguration.GetApiKey());
            antBox.Price = service.GetPrice(type);
            antBox.Secure = service.GetSecure(type);

            switch (type)
            {
                case AntBoxTypeEnum.Little:
                    antBox.Description = WebConfigurationManager.AppSettings["abLitleModel"];
                    antBox.Sizes = WebConfigurationManager.AppSettings["abLitleSize"];
                    break;
                case AntBoxTypeEnum.Big:
                    antBox.Description = WebConfigurationManager.AppSettings["abBigModel"];
                    antBox.Sizes = WebConfigurationManager.AppSettings["abBigSize"];
                    break;
                default:
                    break;
            }


            return antBox;

        }

    }

    public enum AntBoxTypeEnum
    {
        Little,
        Big
    }

}