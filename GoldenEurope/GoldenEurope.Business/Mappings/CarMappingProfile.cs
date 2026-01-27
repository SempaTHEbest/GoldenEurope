using AutoMapper;
using GoldenEurope.Business.DTOs;
using GoldenEurope.Business.DTOs.BrandDto;
using GoldenEurope.Business.DTOs.ModelDto;
using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        // Entity to DTOs
        CreateMap<Car, CarDto>()
            .ForCtorParam(nameof(CarDto.BrandName), opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.Name : "N/A"))
            .ForCtorParam(nameof(CarDto.ModelName), opt => opt.MapFrom(src => src.Model != null ? src.Model.Name : "N/A"))
            .ForCtorParam(nameof(CarDto.Category), opt => opt.MapFrom(src => src.Model != null ? src.Model.Category : "N/A"))
            
            // 3. Enums -> String (Найважливіше!)
            .ForCtorParam(nameof(CarDto.Condition), opt => opt.MapFrom(src => src.Condition.ToString()))
            .ForCtorParam(nameof(CarDto.Fuel), opt => opt.MapFrom(src => src.Fuel.ToString()))
            .ForCtorParam(nameof(CarDto.Transmission), opt => opt.MapFrom(src => src.Transmission.ToString()))
            .ForCtorParam(nameof(CarDto.Body), opt => opt.MapFrom(src => src.Body.ToString()))
            .ForCtorParam(nameof(CarDto.Drivetrain), opt => opt.MapFrom(src => src.Drivetrain.ToString()));
        
        CreateMap<CreateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.IsSold, opt => opt.Ignore());
        
        CreateMap<Brand, BrandDto>();
        CreateMap<CreateBrandDto, Brand>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Models, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
             
        CreateMap<UpdateBrandDto, Brand>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Models, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Model, ModelDto>()
            .ForCtorParam(nameof(ModelDto.BrandName), opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : "N/A"));

        CreateMap<CreateModelDto, Model>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Brand, opt => opt.Ignore())
             .ForMember(dest => dest.Cars, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
             
        CreateMap<UpdateModelDto, Model>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Brand, opt => opt.Ignore())
             .ForMember(dest => dest.Cars, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
             
        CreateMap<CarSearchDto, CarFilter>();
    }
}