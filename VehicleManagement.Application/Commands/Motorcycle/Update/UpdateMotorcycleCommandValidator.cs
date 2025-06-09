using FluentValidation;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Motorcycle.Update;

public class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
{
    public UpdateMotorcycleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(DomainModel.Models.MotorcycleAggregate.Motorcycle.Title)))

            .MaximumLength(100)
            .WithMessage(string.Format(Resources.Messages.MaxLength, nameof(DomainModel.Models.MotorcycleAggregate.Motorcycle.Title), 100));
    }
}
