using AutoMapper;
using GoldenEurope.Business.DTOs;
using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        // Entity to DTOs
        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.Name : "N/A"))
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.Name : "N/A"))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Model != null ? src.Model.Category : "N/A"))
            //For safety 
            .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToString()))
            .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel.ToString()))
            .ForMember(dest => dest.Transmission, opt => opt.MapFrom(src => src.Transmission.ToString()))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body.ToString()))
            .ForMember(dest => dest.Drivetrain, opt => opt.MapFrom(src => src.Drivetrain.ToString()));
        
        // DTOs to entity(Create)
        CreateMap<CreateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.IsSold, opt => opt.Ignore());
        
        // DTOs to entity(Update)
        CreateMap<UpdateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Vin, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore());
        
        //DTOs to filter
        CreateMap<CarSearchDto, CarFilter>();
    }
}
