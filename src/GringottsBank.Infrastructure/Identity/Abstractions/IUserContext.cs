using System;

namespace GringottsBank.Infrastructure.Identity.Abstractions
{
    public interface IUserContext
    {
        Guid? CurrentUserId { get; }
    }
}
