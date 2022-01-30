using FluentValidation;

namespace GringottsBank.Application.Features.Account.Commands.Validators
{
    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(p => p.AccountId)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Amount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
