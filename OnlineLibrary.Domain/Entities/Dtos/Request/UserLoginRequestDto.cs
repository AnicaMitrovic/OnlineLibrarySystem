namespace OnlineLibrary.Domain.Entities.Dtos.Request
{
    public class UserLoginRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
