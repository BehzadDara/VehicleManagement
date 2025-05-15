using FluentValidation;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Motorcycle.Create;

public class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
{
    public CreateMotorcycleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.Motorcycle.Title)))

            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.Motorcycle.Title), 100));
    }
}
