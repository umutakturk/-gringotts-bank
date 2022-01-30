using System.Threading;
using System.Threading.Tasks;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Identity.DTOs;
using GringottsBank.Common.Exceptions;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Features.Identity.Commands.Handlers
{
    public class GenerateTokenCommandHandler : ICommandHandler<GenerateTokenCommand, Result<TokenResponse>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IJwtHelper _jwtHelper;
        private readonly IPasswordHasher _passwordHasher;

        public GenerateTokenCommandHandler(IDatabaseContext dbContext, IJwtHelper jwtHelper, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _jwtHelper = jwtHelper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<TokenResponse>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(p => p.EmailAddress == request.Email, cancellationToken);

            if (customer is null || !_passwordHasher.Verify(customer.Password, request.Password))
            {
                throw new ApplicationException("Your email address and/or password do not match.");
            }

            var jwt = _jwtHelper.Generate(customer.Id);
            return Result.Success(new TokenResponse(jwt));
        }
    }
}
