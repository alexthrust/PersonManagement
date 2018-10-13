using AutoMapper;
using PersonManagement.Data.DbModels;
using PersonManagement.Extensions;
using PersonManagement.Services.Models;

namespace PersonManagement.Api.AutoMapperProfiles
{
    public class ServiceToDataModelsProfile : Profile
    {
        public ServiceToDataModelsProfile()
        {
            CreateMap<PersonModel, Person>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToInt32()));
        }
    }
}