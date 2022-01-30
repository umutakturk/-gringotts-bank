using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Customer.DTOs;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;

namespace GringottsBank.Application.Features.Customer.Commands.Handlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<CustomerResponse>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public CreateCustomerCommandHandler(IDatabaseContext dbContext, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var newCustomer = new Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.Email,
                Password = _passwordHasher.Hash(request.Password)
            };

            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<CustomerResponse>(newCustomer);
            return Result.Success(result);
        }
    }
}
