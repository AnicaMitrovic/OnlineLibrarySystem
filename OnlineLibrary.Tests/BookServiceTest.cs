using AutoMapper;
using Moq;
using OnlineLibrary.Domain.Entities.Dtos.Response;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Infrastructure.Interfaces;
using OnlineLibrary.Infrastructure.Services;
using OnlineLibrary.Domain.Entities.Dtos.Request;

namespace OnlineLibrary.Tests
{
    public class BookServiceTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTest()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mockMapper = new Mock<IMapper>();
            _bookService = new BookService(_bookRepositoryMock.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllBooks_Should_ReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Publisher = "Publisher 1", Timestamp = DateTime.Now },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Publisher = "Publisher 2", Timestamp = DateTime.Now }
            };

            var expectedResponse = new List<BookResponseDto>
            {
                new BookResponseDto { Id = 1, Title = "Book 1", Author = "Author 1", Publisher = "Publisher 1" },
                new BookResponseDto { Id = 2, Title = "Book 2", Author = "Author 2", Publisher = "Publisher 2" }
            };

            _bookRepositoryMock.Setup(repo => repo.GetAllBooks()).ReturnsAsync(books);
            _mockMapper.Setup(mapper => mapper.Map<BookResponseDto>(It.IsAny<Book>()))
                       .Returns((Book b) => expectedResponse.First(r => r.Id == b.Id));

            // Act
            var result = await _bookService.GetAllBooks();

            // Assert
            Assert.Equal(expectedResponse.Count, result.Count);
            Assert.All(result, r => Assert.Contains(expectedResponse, er => er.Id == r.Id));
        }

        [Fact]
        public async Task AddBook_Should_ReturnBookResponseDto_WhenValidRequestIsProvided()
        {
            // Arrange
            var newBookDto = new BookRequestDto { Title = "New Book", Author = "Author", Publisher = "Publisher" };
            var addedBook = new Book { Id = 1, Title = "New Book", Author = "Author", Publisher = "Publisher", Timestamp = DateTime.Now };
            var expectedResponse = new BookResponseDto { Id = 1, Title = "New Book", Author = "Author", Publisher = "Publisher" };

            _bookRepositoryMock.Setup(repo => repo.AddBook(newBookDto)).ReturnsAsync(addedBook);
            _mockMapper.Setup(mapper => mapper.Map<BookResponseDto>(addedBook)).Returns(expectedResponse);

            // Act
            var result = await _bookService.AddBook(newBookDto);

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public async Task AddBook_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var newBookDto = new BookRequestDto { Title = "New Book", Author = "Author", Publisher = "Publisher" };

            _bookRepositoryMock.Setup(repo => repo.AddBook(newBookDto)).ThrowsAsync(new Exception("Database exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _bookService.AddBook(newBookDto));
        }

        [Fact]
        public async Task SearchBooks_ShouldReturnBooks_WhenMatchesAreFound()
        {
            // Arrange
            var searchDto = new SearchBookRequestDto { Title = "SearchTitle" };
            var foundBooks = new List<Book> { new Book { Id = 1, Title = "SearchTitle", Author = "Author1", Publisher = "Publisher1" } };
            var foundBookDtos = new List<BookResponseDto> { new BookResponseDto { Id = 1, Title = "SearchTitle", Author = "Author1", Publisher = "Publisher1" } };

            _bookRepositoryMock.Setup(repo => repo.SearchBooks(searchDto)).ReturnsAsync(foundBooks);
            _mockMapper.Setup(mapper => mapper.Map<BookResponseDto>(It.IsAny<Book>())).Returns<Book>(b => foundBookDtos.First(f => f.Id == b.Id));

            // Act
            var result = await _bookService.SearchBooks(searchDto);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(foundBookDtos.First().Title, result.First().Title);
        }

        [Fact]
        public async Task SearchBooks_ShouldReturnEmptyList_WhenNoMatchesAreFound()
        {
            // Arrange
            var searchDto = new SearchBookRequestDto { Title = "NoMatches" };

            _bookRepositoryMock.Setup(repo => repo.SearchBooks(searchDto)).ReturnsAsync(new List<Book>());

            // Act
            var result = await _bookService.SearchBooks(searchDto);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteBook_Should_ReturnDeletedBookDetails_WhenBookExists()
        {
            // Arrange
            int bookIdToDelete = 1;
            var deletedBook = new Book { Id = bookIdToDelete, Title = "Book Title", Author = "Author", Publisher = "Publisher", Timestamp = DateTime.Now };
            var deletedBookResponse = new BookResponseDto { Id = bookIdToDelete, Title = "Book Title", Author = "Author", Publisher = "Publisher" };

            _bookRepositoryMock.Setup(r => r.DeleteBook(bookIdToDelete))
                               .ReturnsAsync(deletedBook);

            _mockMapper.Setup(m => m.Map<BookResponseDto>(deletedBook))
                       .Returns(deletedBookResponse);

            // Act
            var result = await _bookService.DeleteBook(bookIdToDelete);

            // Assert
            Assert.Equal(deletedBookResponse, result);
        }

        [Fact]
        public async Task DeleteBook_Should_ReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            int bookIdToDelete = 2;
            _bookRepositoryMock.Setup(r => r.DeleteBook(bookIdToDelete))
                               .ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.DeleteBook(bookIdToDelete);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBook_Should_ReturnUpdatedBookDto_WhenBookIsSuccessfullyUpdated()
        {
            // Arrange
            var bookToUpdateDto = new BookRequestDto { Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };
            var updatedBook = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };
            var updatedBookResponseDto = new BookResponseDto { Id = 1, Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };

            _bookRepositoryMock.Setup(repo => repo.UpdateBook(bookToUpdateDto, It.IsAny<int>())).ReturnsAsync(updatedBook);
            _mockMapper.Setup(mapper => mapper.Map<BookResponseDto>(updatedBook)).Returns(updatedBookResponseDto);

            // Act
            var result = await _bookService.UpdateBook(bookToUpdateDto, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBookResponseDto.Title, result.Title);
            Assert.Equal(updatedBookResponseDto.Author, result.Author);
            Assert.Equal(updatedBookResponseDto.Publisher, result.Publisher);
        }

        [Fact]
        public async Task UpdateBook_Should_ReturnNull_WhenNoBookIsFoundToUpdate()
        {
            // Arrange
            var bookToUpdateDto = new BookRequestDto { Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };

            _bookRepositoryMock.Setup(repo => repo.UpdateBook(bookToUpdateDto, It.IsAny<int>())).ReturnsAsync((Book?)null);

            // Act
            var result = await _bookService.UpdateBook(bookToUpdateDto, 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBook_Should_HandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            var bookToUpdateDto = new BookRequestDto { Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };

            _bookRepositoryMock.Setup(repo => repo.UpdateBook(bookToUpdateDto, It.IsAny<int>())).ThrowsAsync(new Exception("Database exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _bookService.UpdateBook(bookToUpdateDto, 1));
        }
    }
}
