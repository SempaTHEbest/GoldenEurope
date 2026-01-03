using System.Data;
using FluentValidation;
using GoldenEurope.Business.DTOs.BrandDto;

namespace GoldenEurope.Business.Validators.BrandValidators;

public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
{
 public CreateBrandDtoValidator()
 {
  RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
  RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
 }   
}