using Moq;
using OnlineLibrary.Domain.Entities;
using AutoMapper;
using OnlineLibrary.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;
using OnlineLibrary.Infrastructure.Services;
using OnlineLibrary.Infrastructure.DataModels;
using OnlineLibrary.Infrastructure.Data;

namespace OnlineLibrary.Tests
{
    public class BookServiceTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock = new Mock<IBookRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly BookService _bookService;

        public BookServiceTest()
        {
            _bookService = new BookService(_bookRepositoryMock.Object, _mapperMock.Object);
        }

        public static List<Book> BookList = new List<Book>
        {
            new Book {Id = 1, Title = "A Brief History of Time", Author = "Stephen Hawking", Publisher = "Bantam Books", Timestamp = new DateTime(2000, 1, 1)},
            new Book {Id = 2, Title = "Cosmos", Author = "Carl Sagan", Publisher = "Random House", Timestamp = new DateTime(2000, 3, 1)},
            new Book {Id = 3, Title = "Astrophysics for People in a Hurry", Author = "Neil deGrasse Tyson", Publisher = "Norton & Company", Timestamp = new DateTime(2005, 1, 6)},
            new Book {Id = 4, Title = "Packing for Mars", Author = "Mary Roach", Publisher = "Norton & Company", Timestamp = new DateTime(2020, 2, 1)},
            new Book {Id = 5, Title = "The Elegant Universe", Author = "Brian Greene", Publisher = "Norton & Company", Timestamp = new DateTime(2022, 1, 17)}
        };

        [Fact]
        public async Task SearchBooks_ReturnsCorrectBooks()
        {
            // Arrange
            var searchBookDto = new SearchBookRequestDto { Title = "Test" };
            var book = new Book { Title = "TestTitle", Author = "TestAuthor" };
            var bookResponseDto = new BookResponseDto { Title = "TestTitle", Author = "TestAuthor" };

            _bookRepositoryMock.Setup(br => br.SearchBooks(It.IsAny<SearchBookRequestDto>()))
                .ReturnsAsync(new List<Book> { book });

            _mapperMock.Setup(m => m.Map<BookResponseDto>(It.IsAny<Book>()))
                .Returns(bookResponseDto);

            // Act
            var result = await _bookService.SearchBooks(searchBookDto);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(bookResponseDto.Title, result.First().Title);
            Assert.Equal(bookResponseDto.Author, result.First().Author);

            _bookRepositoryMock.Verify(br => br.SearchBooks(It.IsAny<SearchBookRequestDto>()), Times.Once());
            _mapperMock.Verify(m => m.Map<BookResponseDto>(It.IsAny<Book>()), Times.Once());
        }

        [Fact]
        public async Task GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var dbContext = new AppDbContext();
            dbContext.BookList = BookList;

            var expectedBooks = new List<Book> {
                new Book {Id = 1, Title = "A Brief History of Time", Author = "Stephen Hawking", Publisher = "Bantam Books", Timestamp = new DateTime(2000, 1, 1)},
                new Book {Id = 2, Title = "Cosmos", Author = "Carl Sagan", Publisher = "Random House", Timestamp = new DateTime(2000, 3, 1)},
                new Book {Id = 3, Title = "Astrophysics for People in a Hurry", Author = "Neil deGrasse Tyson", Publisher = "Norton & Company", Timestamp = new DateTime(2005, 1, 6)},
                new Book {Id = 4, Title = "Packing for Mars", Author = "Mary Roach", Publisher = "Norton & Company", Timestamp = new DateTime(2020, 2, 1)},
                new Book {Id = 5, Title = "The Elegant Universe", Author = "Brian Greene", Publisher = "Norton & Company", Timestamp = new DateTime(2022, 1, 17)}
            };

            var bookRepository = new BookRepository(dbContext);

            // Act
            var result = await bookRepository.GetAllBooks();

            // Assert
            Assert.Equal(expectedBooks.Count, result.Count);
        }
    }
}