using System;
using System.Collections.Generic;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Account.Queries
{
    public record GetTransactionsQuery(Guid AccountId, DateTime StartDate, DateTime EndDate)
        : IQuery<Result<List<TransactionResponse>>>;
}
