using AutoMapper;
using AutoMapper.QueryableExtensions;
using BankApp.Application.DtoObjects;
using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApp.Application
{
    public class AutoMapperInitializer
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Disposition, DispositionDto>();
            CreateMap<Domain.Entities.Account, AccountDto>();
            CreateMap<Card, CardDto>();
            CreateMap<Customer, CustomerDto>(MemberList.Destination)
                .ForSourceMember(x => x.Dispositions, opt => opt.DoNotValidate());
        }
    }
}
