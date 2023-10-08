using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;

namespace OnlineLibrary.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        //Task<ServiceResponse<List<Book>>> AddBook(BookRequestDto newBook);
        Task<List<Book>> SearchBooks(SearchBookRequestDto searchBookDto);
        //Task<ServiceResponse<List<Book>>> DeleteBook(int id);
        //Task<ServiceResponse<List<Book>>> UpdateBook(int id, Book bookToUpdate);
    }
}
