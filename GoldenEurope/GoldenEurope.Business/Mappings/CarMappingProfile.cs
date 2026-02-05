using AutoMapper;
using GoldenEurope.Business.DTOs;
using GoldenEurope.Business.DTOs.BrandDto;
using GoldenEurope.Business.DTOs.ModelDto;
using GoldenEurope.Core.Entities;

namespace GoldenEurope.Business.Mappings;

public class CarMappingProfile : Profile
{
    public CarMappingProfile()
    {
        // --- READ (Entity -> DTO) ---
        
        CreateMap<CarImage, CarImageDto>();

        CreateMap<Car, CarDto>()
            .ForCtorParam(nameof(CarDto.BrandName), opt => opt.MapFrom(src => src.Model != null && src.Model.Brand != null ? src.Model.Brand.Name : "N/A"))
            .ForCtorParam(nameof(CarDto.ModelName), opt => opt.MapFrom(src => src.Model != null ? src.Model.Name : "N/A"))
            .ForCtorParam(nameof(CarDto.Category), opt => opt.MapFrom(src => src.Model != null ? src.Model.Category.ToString() : "N/A"))
            .ForCtorParam(nameof(CarDto.Condition), opt => opt.MapFrom(src => src.Condition.ToString()))
            .ForCtorParam(nameof(CarDto.Fuel), opt => opt.MapFrom(src => src.Fuel.ToString()))
            .ForCtorParam(nameof(CarDto.Transmission), opt => opt.MapFrom(src => src.Transmission.ToString()))
            .ForCtorParam(nameof(CarDto.Body), opt => opt.MapFrom(src => src.Body.ToString()))
            .ForCtorParam(nameof(CarDto.Drivetrain), opt => opt.MapFrom(src => src.Drivetrain.ToString()))
            .ForCtorParam(nameof(CarDto.SellerPhone), opt => opt.MapFrom(src => src.SellerPhone))
            .ForCtorParam(nameof(CarDto.PhoneViewCount), opt => opt.MapFrom(src => src.PhoneViewCount))
            // Мапимо колекцію картинок
            .ForCtorParam(nameof(CarDto.Images), opt => opt.MapFrom(src => src.Images));

        CreateMap<Model, ModelDto>()
            .ForCtorParam(nameof(ModelDto.BrandName), opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : "N/A"))
            .ForCtorParam(nameof(ModelDto.Category), opt => opt.MapFrom(src => src.Category.ToString()));
        
        CreateMap<Brand, BrandDto>();
        
        // --- WRITE (DTO -> Entity) ---
        
        CreateMap<CreateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.IsSold, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneViewCount, opt => opt.Ignore())
            // FIX 500 ERROR: Ігноруємо Images, бо в DTO це файли, а ми їх обробляємо вручну в Service
            .ForMember(dest => dest.Images, opt => opt.Ignore()) 
            .ForMember(dest => dest.SellerPhone, opt => opt.MapFrom(src => src.SellerPhone));
            
        CreateMap<UpdateCarDto, Car>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Vin, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Model, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore())
            .ForMember(dest => dest.SellerPhone, opt => opt.MapFrom(src => src.SellerPhone));

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