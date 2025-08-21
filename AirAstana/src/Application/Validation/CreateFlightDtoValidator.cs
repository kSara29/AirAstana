using Application.Models.DTO;
using Domain.Enums;
using FluentValidation;

namespace Application.Validation;

public class CreateFlightDtoValidator : AbstractValidator<CreateFlightDto>
{
    public CreateFlightDtoValidator()
    {
        RuleFor(x => x.Origin).NotEmpty();
        RuleFor(x => x.Destination)
            .NotEmpty().MaximumLength(256)
            .NotEqual(x => x.Origin)
            .WithMessage("Пункт назначения должен отличаться от пункта отправки");

        RuleFor(x => x.Departure).NotEmpty();
        RuleFor(x => x.Arrival).NotEmpty();

        var allowed = string.Join(", ",
            Enum.GetValues<FlightStatus>()
                .Where(v => !EqualityComparer<FlightStatus>.Default.Equals(v, default))
                .Select(v => v.ToString()));
        
        RuleFor(x => x.Status)
            .IsInEnum()
            .NotEqual(FlightStatus.Undefined)
            .WithMessage("Присвойте один из следующих статусов: " + allowed);
    }
}