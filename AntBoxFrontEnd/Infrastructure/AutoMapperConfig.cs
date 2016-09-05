using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Customer;
using static AutoMapper.Mapper;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Models;

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

            CreateMap<AddressRequestOptions, AntBoxAddressViewModel>();

            CreateMap<AntBoxAddressViewModel, AddressRequestOptions>();

            CreateMap<AddressResponse, AntBoxAddressViewModel>();

            CreateMap<AntBoxAddressViewModel, AddressUpdateOptions>();

        }

    }
}