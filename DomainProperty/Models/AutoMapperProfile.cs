using AutoMapper;
using DomainProperty.Models.DataModels;
using DomainProperty.Models.DtoModels;
using Microsoft.Extensions.Logging;

namespace DomainProperty.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Property, PropertyDTO>();
        }
    }
}
