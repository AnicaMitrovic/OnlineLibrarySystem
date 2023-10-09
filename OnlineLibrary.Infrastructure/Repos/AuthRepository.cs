using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineLibrary.Infrastructure.Repos
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;

        public AuthRepository(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<User?> Login(string username, string password)
        {
            User? user = _db.Users
                .FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));

            return await Task.FromResult(user);

            //if (user is null)
            //{
            //    response.Success = false;
            //    response.Message = "User not found.";
            //}
            //else if (!VerifyPassword(password, user.Password))
            //{
            //    response.Success = false;
            //    response.Message = "Wrong password.";
            //}
            //else
            //{
            //    response.Data = CreateToken(user);
            //}

            //return await Task.FromResult(response);
        }
    }
}
