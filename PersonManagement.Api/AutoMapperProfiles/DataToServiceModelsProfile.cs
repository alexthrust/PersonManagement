using System;
using AutoMapper;
using PersonManagement.Constants;
using PersonManagement.Data.DbModels;
using PersonManagement.Services.Models;
using PersonManagement.Utils;

namespace PersonManagement.Api.AutoMapperProfiles
{
    public class DataToServiceModelsProfile : Profile
    {
        public DataToServiceModelsProfile()
        {
            CreateMap<Person, PersonModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.HasValue ? (EGender)src.Gender : EGender.None))
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => EnumHelper.GetPersonGenderName(src.Gender)));
        }
    }
}