using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Features.Account.Queries.Handlers
{
    public class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, Result<List<TransactionResponse>>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IDatabaseContext dbContext, IUserContext userContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Result<List<TransactionResponse>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .Where(p => p.Customer.Id == _userContext.CurrentUserId && p.Id == request.AccountId)
                .Include(i => i.Transactions
                    .Where(t => t.CreatedAt >= request.StartDate && t.CreatedAt <= request.EndDate)
                    .OrderByDescending(o => o.CreatedAt))
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<List<TransactionResponse>>(account.Transactions);
            return Result.Success(result);
        }
    }
}
