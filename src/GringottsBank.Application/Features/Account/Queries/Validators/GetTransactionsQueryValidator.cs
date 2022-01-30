using FluentValidation;

namespace GringottsBank.Application.Features.Account.Queries.Validators
{
    public class GetTransactionsQueryValidator : AbstractValidator<GetTransactionsQuery>
    {
        public GetTransactionsQueryValidator()
        {
            RuleFor(p => p.AccountId)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.StartDate)
                .NotEmpty();

            RuleFor(p => p.EndDate)
                .NotEmpty();

        }
    }
}
