using Application.Models.DTO;
using FluentValidation;

namespace Application.Validation;

public class FlightsValidator : AbstractValidator<FlightDto>
{
    public FlightsValidator()
    {
        RuleFor(x => x.Origin).MaximumLength(256);
        RuleFor(x => x.Destination).MaximumLength(256);
        
        When(x => !string.IsNullOrWhiteSpace(x.Origin) && !string.IsNullOrWhiteSpace(x.Destination), () =>
        {
            RuleFor(x => x.Destination)
                .NotEqual(x => x.Origin)
                .WithMessage("Пункт назначения должен отличаться от пункта отправки");
        });
    }
}