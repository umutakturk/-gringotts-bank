using System.Collections.Generic;
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
    public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, Result<List<AccountResponse>>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IDatabaseContext dbContext, IUserContext userContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Result<List<AccountResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .Include(i => i.Accounts)
                .FirstOrDefaultAsync(p => p.Id == _userContext.CurrentUserId, cancellationToken);

            var result = _mapper.Map<List<AccountResponse>>(customer.Accounts);
            return Result.Success(result);
        }
    }
}
