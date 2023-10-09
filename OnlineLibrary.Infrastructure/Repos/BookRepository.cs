using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Interfaces;

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

        public async Task<Book> AddBook(BookRequestDto newBookDto)
        {
            int id = _db.BookList.OrderByDescending(b => b.Id).FirstOrDefault()!.Id + 1;
            Book bookToAdd = new Book
            {
                Id = id,
                Title = newBookDto.Title,
                Author = newBookDto.Author,
                Publisher = newBookDto.Publisher,
                Timestamp = DateTime.Now
            };

            _db.BookList.Add(bookToAdd);

            return await Task.FromResult(bookToAdd);
        }

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

        public async Task<Book?> DeleteBook(int id)
        {
            var bookToDelete = _db.BookList.Where(book => book.Id == id).FirstOrDefault();
            if (bookToDelete is not null)
            {
                _db.BookList.RemoveAll(b => b.Id == bookToDelete.Id);
                return await Task.FromResult(bookToDelete);
            }
            return null;
        }

        public async Task<Book?> UpdateBook(BookRequestDto bookToUpdateDto, int id)
        {
            var bookToUpdate = _db.BookList.FirstOrDefault(b => b.Id == id);
            if (bookToUpdate is null)
            {
                //return await Task.FromResult(new ServiceResponse<List<Book>>
                //{
                //    Message = $"No book found with ID {id}",
                //    Success = false
                //});
                return null;
            }

            bookToUpdate.Title = bookToUpdateDto.Title;
            bookToUpdate.Author = bookToUpdateDto.Author;
            bookToUpdate.Publisher = bookToUpdateDto.Publisher;

            return await Task.FromResult(bookToUpdate);
        }
    }
}
