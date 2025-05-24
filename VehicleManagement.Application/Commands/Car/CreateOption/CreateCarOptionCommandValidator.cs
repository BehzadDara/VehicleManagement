using FluentValidation;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.CreateOption;

public class CreateCarOptionCommandValidator : AbstractValidator<CreateCarOptionCommand>
{
    public CreateCarOptionCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(CarOption.Description)));
    }
}
