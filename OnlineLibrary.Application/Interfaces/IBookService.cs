using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;

namespace OnlineLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetAllBooks();
        Task<BookResponseDto> AddBook(BookRequestDto bookRequestDto);
        Task<List<BookResponseDto>> SearchBooks(SearchBookRequestDto searchBookDto);
        Task<BookResponseDto> DeleteBook(int id);
        Task<ServiceResponse<BookResponseDto>> UpdateBook(BookRequestDto bookToUpdate, int id);
    }
}
