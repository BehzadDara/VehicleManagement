using FluentValidation;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.Create;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.CarAggregate.Car.Title)))

            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.CarAggregate.Car.Title), 100));
    }
}
