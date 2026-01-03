using FluentValidation;
using GoldenEurope.Business.DTOs.BrandDto;

namespace GoldenEurope.Business.Validators.BrandValidators;

public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
    }
}