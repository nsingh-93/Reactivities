using Application.Profiles.DTOs;
using FluentValidation;

namespace Application.Profiles.Validators;

public class BaseProfileValidator<T, TDto> : AbstractValidator<T>
    where TDto : ProfileDto
{
    public BaseProfileValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).DisplayName).NotEmpty().WithMessage("Display Name is required");
    }
}
