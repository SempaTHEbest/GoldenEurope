using FluentValidation;
using GoldenEurope.Business.DTOs;

namespace GoldenEurope.Business.Validators;

public class CreateCarDtoValidator :  AbstractValidator<CreateCarDto>
{
    public CreateCarDtoValidator()
    {
        RuleFor(x => x.Vin)
            .NotEmpty().WithMessage("Vin cannot be empty")
            .Length(17).WithMessage("Vin must be 17 characters long");
        
        RuleFor(x => x.Year)
            .GreaterThan(1900).WithMessage("Year cannot be less than 1900")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 1).WithMessage("Year cannot be greater than DateTime.UtcNow.Year");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price cannot be less than 0");
        
        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0).WithMessage("Mileage cannot be less than 0");
        
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters");

        RuleFor(x => x.SellerPhone)
            .NotEmpty().WithMessage("Seller phone cannot be empty")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number must be valid (e.g. +380501234567)");

        RuleFor(x => x.Fuel).IsInEnum();
        RuleFor(x => x.Transmission).IsInEnum();
        RuleFor(x => x.Condition).IsInEnum();
    }
}