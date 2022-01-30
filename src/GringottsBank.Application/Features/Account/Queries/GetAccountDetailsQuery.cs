using System;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Account.Queries
{
    public record GetAccountDetailsQuery(Guid AccountId) : IQuery<Result<AccountResponse>>;
}
