using System;

namespace GringottsBank.Infrastructure.Identity.Abstractions
{
    public interface IJwtHelper
    {
        string Generate(Guid id);
    }
}
