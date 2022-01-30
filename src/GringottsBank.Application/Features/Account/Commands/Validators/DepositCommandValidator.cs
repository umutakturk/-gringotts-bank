using FluentValidation;

namespace GringottsBank.Application.Features.Account.Commands.Validators
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(p => p.AccountId)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Amount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
