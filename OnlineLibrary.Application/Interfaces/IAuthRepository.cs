using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> Login(string username, string password);
    }
}
