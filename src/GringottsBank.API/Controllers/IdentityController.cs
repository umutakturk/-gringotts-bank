using System.Threading.Tasks;
using GringottsBank.Application.Features.Identity.Commands;
using GringottsBank.Application.Features.Identity.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.API.Controllers
{
    public class IdentityController : BaseController
    {
        public IdentityController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequest request)
        {
            var result = await Mediator.Send(new GenerateTokenCommand(request.Email, request.Password));
            return Ok(result);
        }
    }
}
