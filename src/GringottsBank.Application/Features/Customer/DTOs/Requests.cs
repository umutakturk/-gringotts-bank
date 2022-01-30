namespace GringottsBank.Application.Features.Customer.DTOs
{
    public record CreateCustomerRequest(string FirstName, string LastName, string Email, string Password);
}
