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
        // -----------------------------------------------------------
        // READ: Entity -> DTO (Records)
        // Використовуємо .ToString(), щоб перетворити Enum в текст
        // -----------------------------------------------------------

        CreateMap<Car, CarDto>()
            // Nested Objects
            .ForCtorParam(nameof(CarDto.BrandName), opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.Name : "N/A"))
            .ForCtorParam(nameof(CarDto.ModelName), opt => opt.MapFrom(src => src.Model != null ? src.Model.Name : "N/A"))
            
            // !!! FIX: Беремо Category з Model (Enum) і перетворюємо в String
            .ForCtorParam(nameof(CarDto.Category), opt => opt.MapFrom(src => src.Model != null ? src.Model.Category.ToString() : "N/A"))
            
            // Car Enums -> String
            .ForCtorParam(nameof(CarDto.Condition), opt => opt.MapFrom(src => src.Condition.ToString()))
            .ForCtorParam(nameof(CarDto.Fuel), opt => opt.MapFrom(src => src.Fuel.ToString()))
            .ForCtorParam(nameof(CarDto.Transmission), opt => opt.MapFrom(src => src.Transmission.ToString()))
            .ForCtorParam(nameof(CarDto.Body), opt => opt.MapFrom(src => src.Body.ToString()))
            .ForCtorParam(nameof(CarDto.Drivetrain), opt => opt.MapFrom(src => src.Drivetrain.ToString()));

        CreateMap<Model, ModelDto>()
            .ForCtorParam(nameof(ModelDto.BrandName), opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : "N/A"))
            // !!! FIX: Model.Category (Enum) -> ModelDto.Category (String)
            .ForCtorParam(nameof(ModelDto.Category), opt => opt.MapFrom(src => src.Category.ToString()));
        
        CreateMap<Brand, BrandDto>();


        // -----------------------------------------------------------
        // WRITE: DTO (Records) -> Entity (Class)
        // Тут Enum лягає в Enum автоматично по назві
        // -----------------------------------------------------------

        // Create Car
        CreateMap<CreateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.IsSold, opt => opt.Ignore());
            
        // Update Car (Тут у тебе в коді його не було в останньому повідомленні, але я додам базовий)
        CreateMap<UpdateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Vin, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore());

        // Create Brand
        CreateMap<CreateBrandDto, Brand>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Models, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
             
        // Update Brand
        CreateMap<UpdateBrandDto, Brand>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Models, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Create Model
        CreateMap<CreateModelDto, Model>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Brand, opt => opt.Ignore())
             .ForMember(dest => dest.Cars, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
             // Category (Enum) мапиться автоматично, бо імена співпадають
             
        // Update Model
        CreateMap<UpdateModelDto, Model>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Brand, opt => opt.Ignore())
             .ForMember(dest => dest.Cars, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Filter Mapping
        CreateMap<CarSearchDto, CarFilter>();
    }
}