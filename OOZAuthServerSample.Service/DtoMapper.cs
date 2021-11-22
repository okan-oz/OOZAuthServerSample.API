using System;
using AutoMapper;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Model;

namespace OOZAuthServerSample.Service
{
    internal class DtoMapper:Profile
    {
       public DtoMapper()
        {

            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
