using FluentValidation;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.CreateTag;

public class CreateCarTagCommandValidator : AbstractValidator<CreateCarTagCommand>
{
    public CreateCarTagCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.CarTag.Title)));
    }
}
