using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Application.Commands.Car.CreateTag;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.DeleteTag;

public class DeleteCarTagCommandValidator : AbstractValidator<DeleteCarTagCommand>
{
    public DeleteCarTagCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(CarTag.Title)));

        RuleFor(x => x.Priority)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Resources.Messages.Required, nameof(CarTag.Priority)));
    }
}
