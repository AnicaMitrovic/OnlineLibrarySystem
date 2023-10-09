using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> Login(string username, string password);
    }
}
