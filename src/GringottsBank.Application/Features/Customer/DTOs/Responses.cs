using System;

namespace GringottsBank.Application.Features.Customer.DTOs
{
    public record CustomerResponse(Guid Id, string FirstName, string LastName, string EmailAddress, DateTime CreatedAt);
}