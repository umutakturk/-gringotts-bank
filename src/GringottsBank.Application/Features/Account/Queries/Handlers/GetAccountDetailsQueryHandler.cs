using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GringottsBank.Application.Abstractions;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Common.Exceptions;
using GringottsBank.Common.Models;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Features.Account.Queries.Handlers
{
    public class GetAccountDetailsQueryHandler : IQueryHandler<GetAccountDetailsQuery, Result<AccountResponse>>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public GetAccountDetailsQueryHandler(IDatabaseContext dbContext, IUserContext userContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Result<AccountResponse>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(p => p.Customer.Id == _userContext.CurrentUserId && p.Id == request.AccountId, cancellationToken);

            if (account is null)
            {
                throw new NotFoundException(request.AccountId);
            }

            var result = _mapper.Map<AccountResponse>(account);
            return Result.Success(result);
        }
    }
}
