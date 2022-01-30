using System;
using System.Security.Claims;
using GringottsBank.Infrastructure.Identity.Abstractions;
using Microsoft.AspNetCore.Http;

namespace GringottsBank.Infrastructure.Identity
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid? CurrentUserId
        {
            get
            {
                var claim = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                return claim is null
                    ? null
                    : new Guid(claim.Value);
            }
        }
    }
}
