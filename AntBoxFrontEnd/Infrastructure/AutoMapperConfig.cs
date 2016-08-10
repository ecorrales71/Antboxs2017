using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Customer;
using static AutoMapper.Mapper;

namespace AntBoxFrontEnd.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            ConfigureCustomerMapping();


        }


        private static void ConfigureCustomerMapping()
        {
            CreateMap<Customer, CustomerResponse>();
        }

    }
}