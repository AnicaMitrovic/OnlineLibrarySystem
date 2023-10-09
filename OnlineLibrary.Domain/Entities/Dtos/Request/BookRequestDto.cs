using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Domain.Entities.Dtos.Request
{
    public class BookRequestDto
    {
        //[Required]
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
    }
}
