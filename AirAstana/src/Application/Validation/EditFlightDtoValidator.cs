using Application.Models.DTO;
using Domain.Enums;
using FluentValidation;

namespace Application.Validation;

public class EditFlightDtoValidator : AbstractValidator<EditFlightDto>
{
    public EditFlightDtoValidator()
    {
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