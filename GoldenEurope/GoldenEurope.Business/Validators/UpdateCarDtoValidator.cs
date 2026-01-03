using FluentValidation;
using GoldenEurope.Business.DTOs;

namespace GoldenEurope.Business.Validators;

public class UpdateCarDtoValidator : AbstractValidator<UpdateCarDto>
{
    public UpdateCarDtoValidator()
    {
        RuleFor(x => x.Year)
            .GreaterThan(1900)
            .LessThanOrEqualTo(DateTime.Now.Year + 1);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description)
            .MaximumLength(1000);
        RuleFor(x => x.Fuel)
            .IsInEnum();
        RuleFor(x => x.Transmission).IsInEnum();
        RuleFor(x => x.Condition).IsInEnum();
        RuleFor(x => x.Body).IsInEnum();
        RuleFor(x => x.Drivetrain).IsInEnum();
    }
}