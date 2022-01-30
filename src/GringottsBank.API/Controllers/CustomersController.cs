using System.Threading.Tasks;
using GringottsBank.Application.Features.Customer.Commands;
using GringottsBank.Application.Features.Customer.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.API.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
        {
            var result = await Mediator.Send(new CreateCustomerCommand(request.FirstName, request.LastName, request.Email, request.Password));
            return Created("Create", result);
        }
    }
}
