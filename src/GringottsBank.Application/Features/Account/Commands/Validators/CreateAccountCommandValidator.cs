using FluentValidation;

namespace GringottsBank.Application.Features.Account.Commands.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Balance)
                .GreaterThanOrEqualTo(0);
        }
    }
}
