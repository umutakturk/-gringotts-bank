using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Features.Account.Commands.Handlers
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Result<AccountResponse>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IDatabaseContext dbContext, IUserContext userContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Result<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(p => p.Id == _userContext.CurrentUserId, cancellationToken);

            var account = new Domain.Entities.Account
            {
                Name = request.Name,
                Balance = request.Balance,
                Customer = customer
            };

            _dbContext.Accounts.Add(account);
            _dbContext.Transactions.Add(new Domain.Entities.Transaction()
            {
                AccountId = account.Id,
                Type = Domain.Types.TransactionType.Initial,
                Amount = account.Balance
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<AccountResponse>(account);
            return Result.Success(result);
        }
    }
}
