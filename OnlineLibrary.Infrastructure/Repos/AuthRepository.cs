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

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = _db.Users
                .FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));

            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPassword(password, user.Password))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return await Task.FromResult(response);
        }

        private bool VerifyPassword(string password, string savedPassword)
        {
            return password == savedPassword;
        }

        private string CreateToken(User user)
        {
            //declare a list of claims 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            //secret key from appsettings.json file
            var appsettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            if (appsettingsToken is null)
            {
                throw new Exception("AppSettings Token is null!");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appsettingsToken));

            // create signing credentials where we  use sha512 signature algorithm
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // security token descripter - gets the information that is used to create the final token
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            // Jwt package provides support for creating, serializing and validationg JSON Web tokens.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescripter);

            // serialize the security token into a JSON web token and in the end we return exactly that

            return tokenHandler.WriteToken(token);
        }
    }
}
