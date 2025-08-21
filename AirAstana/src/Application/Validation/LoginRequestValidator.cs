using Application.Models;
using FluentValidation;

namespace Application.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Имя пользователя не может быть пустым");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Пароль пользователя не может быть пустым");
    }
}