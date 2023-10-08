using AutoMapper;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Interfaces;
using System.Runtime.CompilerServices;

namespace OnlineLibrary.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _db;

        public BookRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await Task.FromResult(_db.BookList);
        }

        //public async Task<ServiceResponse<List<Book>>> AddBook(BookRequestDto newBook)
        //{
        //    newBook.Id = _db.BookList.OrderByDescending(b => b.Id).FirstOrDefault()!.Id + 1;
        //    bookList.Add(newBook);

        //    var serviceResponse = new ServiceResponse<List<Book>>
        //    {
        //        Data = bookList,
        //        Message = $"Book with ID {newBook.Id} has been added",
        //        Success = true
        //    };

        //    return await Task.FromResult(serviceResponse);
        //}

        public async Task<List<Book>> SearchBooks(SearchBookRequestDto searchBookDto)
        {
            var foundBooks = _db.BookList
                .Where(book =>
                    (string.IsNullOrWhiteSpace(searchBookDto.Title) || book.Title.Contains(searchBookDto.Title, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(searchBookDto.Author) || book.Author.Equals(searchBookDto.Author)) &&
                    (string.IsNullOrWhiteSpace(searchBookDto.Publisher) || book.Publisher.Equals(searchBookDto.Publisher))
                )
                .ToList();

            return await Task.FromResult(foundBooks);
        }

        //public async Task<ServiceResponse<List<Book>>> DeleteBook(int id)
        //{
        //    bookList.RemoveAll(b => b.Id == id);

        //    var serviceResponse = new ServiceResponse<List<Book>>
        //    {
        //        Data = bookList,
        //        Message = $"Book with ID {id} has been removed",
        //        Success = true
        //    };

        //    return await Task.FromResult(serviceResponse);
        //}

        //public async Task<ServiceResponse<List<Book>>> UpdateBook(int id, Book bookToUpdate)
        //{
        //    var book = bookList.FirstOrDefault(b => b.Id == id);
        //    if (book == null)
        //    {
        //        return await Task.FromResult(new ServiceResponse<List<Book>>
        //        {
        //            Message = $"No book found with ID {id}",
        //            Success = false
        //        });
        //    }

        //    book.Title = bookToUpdate.Title;
        //    book.Author = bookToUpdate.Author;
        //    book.Publisher = bookToUpdate.Publisher;

        //    var serviceResponse = new ServiceResponse<List<Book>>
        //    {
        //        Data = bookList,
        //        Message = $"Book with ID {id} has been updated",
        //        Success = true
        //    };

        //    return await Task.FromResult(serviceResponse);
        //}        
    }
}
