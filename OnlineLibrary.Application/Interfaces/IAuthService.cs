using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
