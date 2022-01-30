using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Exceptions;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Features.Account.Commands.Handlers
{
    public class DepositCommandHandler : ICommandHandler<DepositCommand, Result<AccountResponse>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public DepositCommandHandler(IDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<AccountResponse>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _dbContext.Accounts
                    .Include(i => i.Transactions)
                    .FirstOrDefaultAsync(p => p.Id == request.AccountId, cancellationToken);

                if (account is null)
                {
                    throw new NotFoundException(request.AccountId);
                }

                account.Deposit(request.Amount);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<AccountResponse>(account);
                return Result.Success(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConcurrencyException();
            }
        }
    }
}
