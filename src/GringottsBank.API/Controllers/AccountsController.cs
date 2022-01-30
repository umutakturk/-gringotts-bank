using System;
using System.Threading.Tasks;
using GringottsBank.Application.Features.Account.Commands;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Application.Features.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.API.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await Mediator.Send(new GetAccountsQuery());
            return Ok(result);
        }

        [HttpGet("{accountId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAccountDetails(Guid accountId)
        {
            var result = await Mediator.Send(new GetAccountDetailsQuery(accountId));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var result = await Mediator.Send(new CreateAccountCommand(request.Name, request.Balance));
            return Created("Create", result);
        }

        [HttpPut("{accountId:guid}/deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Deposit(Guid accountId, [FromBody] DepositRequest request)
        {
            var result = await Mediator.Send(new DepositCommand(accountId, request.Amount));
            return Ok(result);
        }

        [HttpPut("{accountId:guid}/withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] WithdrawRequest request)
        {
            var result = await Mediator.Send(new WithdrawCommand(accountId, request.Amount));
            return Ok(result);
        }

        [HttpGet("{accountId:guid}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTransactions(Guid accountId, [FromQuery] TransactionsRequest request)
        {
            var result = await Mediator.Send(new GetTransactionsQuery(accountId, request.StartDate, request.EndDate));
            return Ok(result);
        }
    }
}
