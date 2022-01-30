using System;

namespace GringottsBank.Application.Features.Account.DTOs
{
    public record AccountResponse(Guid Id, decimal Balance, DateTime CreatedAt, DateTime LastUpdatedAt);

    public record TransactionResponse(Guid Id, decimal Amount, string Type, DateTime CreatedAt);
}