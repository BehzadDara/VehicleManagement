using FluentValidation;

namespace VehicleManagement.Application.Commands.BackOfficeUser.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(DomainModel.Models.BackOfficeUserAggregate.BackOfficeUser.Username)));
    
        RuleFor(x => x.Password)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(DomainModel.Models.BackOfficeUserAggregate.BackOfficeUser.Password)));
    }
}
