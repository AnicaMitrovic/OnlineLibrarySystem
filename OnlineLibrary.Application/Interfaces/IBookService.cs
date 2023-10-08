using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;

namespace OnlineLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetAllBooks();
        //Task<ServiceResponse<List<Book>>> AddBook(BookRequestDto newBook);
        Task<List<BookResponseDto>> SearchBooks(SearchBookRequestDto searchBook);

        //Task<ServiceResponse<List<Book>>> DeleteBook(int id);
        //Task<ServiceResponse<List<Book>>> UpdateBook(int id, Book bookToUpdate);
    }
}
