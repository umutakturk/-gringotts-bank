namespace GringottsBank.Infrastructure.Identity.Abstractions
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
