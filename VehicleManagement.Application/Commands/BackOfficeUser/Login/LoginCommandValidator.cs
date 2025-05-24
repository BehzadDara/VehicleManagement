using FluentValidation;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.BackOfficeUser.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.BackOfficeUserAggregate.BackOfficeUser.Username)));
    
        RuleFor(x => x.Password)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.BackOfficeUserAggregate.BackOfficeUser.Password)));
    }
}
