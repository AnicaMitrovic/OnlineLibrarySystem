using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Entities.Dtos.Request
{
    public class UserLoginRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
