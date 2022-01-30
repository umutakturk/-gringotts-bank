using System;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Account.Commands
{
    public record DepositCommand(Guid AccountId, decimal Amount) : ICommand<Result<AccountResponse>>;
}
