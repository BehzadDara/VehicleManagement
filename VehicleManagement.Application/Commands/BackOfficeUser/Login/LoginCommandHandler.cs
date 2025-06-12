using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService.Data;
using VehicleManagement.DomainService.Services;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Commands.BackOfficeUser.Login;

public class LoginCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var specification = new GetBackOfficeUserWithUsernameAndPasswordSpecification(request.Username, request.Password);
        var backOfficeUser = await unitOfWork.BackOfficeUserRepository.GetAsync(specification, cancellationToken)
            ?? throw new NotFoundException(Resources.Messages.LoginFailed);

        backOfficeUser.SetLastLoginAt();

        unitOfWork.BackOfficeUserRepository.Update(backOfficeUser);
        await unitOfWork.CommitAsync(cancellationToken);

        var permissions = backOfficeUser.Roles.SelectMany(x => x.Permissions).Select(x => x.Type.ToString()).ToList();

        var token = tokenService.Generate(backOfficeUser.Username, permissions);
        return token;
    }
}
