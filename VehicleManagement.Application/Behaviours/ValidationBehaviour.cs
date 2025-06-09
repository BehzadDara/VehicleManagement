using FluentValidation;
using FluentValidation.Results;
using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await validators.First().ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = Serialize(validationResult.Errors);
                throw new BadRequestException(Resources.Messages.BadRequest, errors);
            }
        }

        return await next(cancellationToken);
    }

    private static Dictionary<string, string[]> Serialize(IEnumerable<ValidationFailure> failures)
    {
        var errors = failures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(failure => failure.ErrorMessage).ToArray()
            );

        return errors;
    }
}
