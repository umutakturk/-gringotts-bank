using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Customer.DTOs;
using GringottsBank.Common.Models;

namespace GringottsBank.Application.Features.Customer.Commands
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email, string Password)
        : ICommand<Result<CustomerResponse>>;
}
