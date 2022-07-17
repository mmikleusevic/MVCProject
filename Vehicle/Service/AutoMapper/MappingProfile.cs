using AutoMapper;
using Service.Models;
using Service.Models.Dto;

namespace Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDto>()
                .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abrv))
                    .ReverseMap();
            CreateMap<VehicleModel, VehicleModelDto>()
                .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abrv)).ReverseMap()
                    .ReverseMap();
        }
    }
}
