using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;

namespace OnlineLibrary.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> AddBook(BookRequestDto newBookDto);
        Task<List<Book>> SearchBooks(SearchBookRequestDto searchBookDto);
        Task<Book?> DeleteBook(int id);
        Task<Book?> UpdateBook(BookRequestDto bookToUpdateDto, int id);
    }
}
