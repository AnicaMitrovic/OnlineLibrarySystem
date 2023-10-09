using Microsoft.Extensions.Configuration;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OnlineLibrary.Application.Interfaces;

namespace OnlineLibrary.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepo, IConfiguration configuration)
        {
            _authRepository = authRepo;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            User? user = await _authRepository.Login(username, password);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = user == null ? "User not found." : "Wrong password."
                };
            }

            var token = CreateToken(user);
            return new ServiceResponse<string> { Data = token };
        }

        private bool VerifyPassword(string password, string savedPassword)
        {
            return password == savedPassword;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var appsettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            if (appsettingsToken is null)
            {
                throw new Exception("AppSettings Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appsettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }
    }
}
