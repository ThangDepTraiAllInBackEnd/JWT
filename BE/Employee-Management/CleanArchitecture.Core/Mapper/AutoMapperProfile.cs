using AutoMapper;
using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeImport>()
            .ForMember(dest => dest.IsEmployeeValid, opt => opt.Ignore())
            .ForMember(dest => dest.IsInsertSuccess, opt => opt.Ignore())
            .ForMember(dest => dest.ImportKey, opt => opt.Ignore())
            .ForMember(dest => dest.ErrorList, opt => opt.Ignore())
            .ReverseMap();
            ;
        }
    }
}
