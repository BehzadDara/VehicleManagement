using FluentValidation;
using Shirin.Resources;

namespace VehicleManagement.Application.Commands.ModelA.Update;

public class UpdateModelACommandValidator : AbstractValidator<UpdateModelACommand>
{
    public UpdateModelACommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.ModelA.Title)))

            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.ModelA.Title), 100));

        RuleFor(x => x.Id)
            .GreaterThan(5)
            .WithMessage(string.Format(Messages.MinValue, nameof(DomainModel.Models.ModelA.Id), 5));
    }
}
