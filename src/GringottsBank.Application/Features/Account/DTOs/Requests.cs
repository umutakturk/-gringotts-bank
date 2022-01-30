using System;

namespace GringottsBank.Application.Features.Account.DTOs
{
    public record CreateAccountRequest(string Name, decimal Balance);

    public record DepositRequest(decimal Amount);

    public record TransactionsRequest(DateTime StartDate, DateTime EndDate);

    public record WithdrawRequest(decimal Amount);
}
