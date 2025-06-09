using FluentValidation;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.CreateTag;

public class CreateCarTagCommandValidator : AbstractValidator<CreateCarTagCommand>
{
    public CreateCarTagCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(CarTag.Title)));
    }
}
