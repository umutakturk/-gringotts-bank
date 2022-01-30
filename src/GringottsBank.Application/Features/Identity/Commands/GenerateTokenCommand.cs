using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Identity.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Identity.Commands
{
    public record GenerateTokenCommand(string Email, string Password)
        : ICommand<Result<TokenResponse>>;
}
