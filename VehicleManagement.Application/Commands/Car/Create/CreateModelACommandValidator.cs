using FluentValidation;
using Shirin.Resources;

namespace VehicleManagement.Application.Commands.ModelA.Create;

public class CreateModelACommandValidator : AbstractValidator<CreateModelACommand>
{
    public CreateModelACommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.ModelA.Title)))

            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.ModelA.Title), 100));
    }
}
