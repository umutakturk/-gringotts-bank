using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Account.Commands
{
    public record CreateAccountCommand(string Name, decimal Balance) : ICommand<Result<AccountResponse>>;
}
