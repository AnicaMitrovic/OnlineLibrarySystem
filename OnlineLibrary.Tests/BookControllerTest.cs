using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.Entities.Dtos.Response;
using OnlineLibrarySystem.Api.Controllers;
using FluentAssertions;
using OnlineLibrary.Domain.Entities.Dtos.Request;

namespace OnlineLibrary.Tests
{
    public class BookControllerTest
    {
        private readonly BooksController _controller;
        private readonly Mock<IBookService> _bookServiceMock;

        public BookControllerTest()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BooksController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task GetAllBooks_Should_ReturnFullBookResponseDtoList()
        {
            // Arrange
            var books = new List<BookResponseDto>
            {
                new BookResponseDto{Id = 1, Title = "Book1", Author = "Author1", Publisher = "Publisher1"},
                new BookResponseDto{Id = 2, Title = "Book2", Author = "Author2", Publisher = "Publisher2"},
            };

            _bookServiceMock.Setup(s => s.GetAllBooks()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult); // Ensure that the cast did not return null
            Assert.Equal(books, okResult.Value as List<BookResponseDto>);
        }

       
        [Fact]
        public async Task SearchBooks_Should_ReturnBookResponseDtoList()
        {
            // Arrange
            var searchRequest = new SearchBookRequestDto { Title = "Book1" };
            var books = new List<BookResponseDto>
            {
                new BookResponseDto{Id = 1, Title = "Book1", Author = "Author1", Publisher = "Publisher1"}
            };

            _bookServiceMock.Setup(s => s.SearchBooks(searchRequest)).ReturnsAsync(books);

            // Act
            var result = await _controller.SearchBooks(searchRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(books);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnOkResult_WithDeletedBookDetails()
        {
            // Arrange
            int bookIdToDelete = 1;
            var deletedBookResponse = new BookResponseDto { Id = bookIdToDelete, Title = "Book Title", Author = "Author", Publisher = "Publisher" };

            _bookServiceMock.Setup(s => s.DeleteBook(bookIdToDelete))
                            .ReturnsAsync(deletedBookResponse);

            // Act
            var result = await _controller.DeleteBook(bookIdToDelete);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(deletedBookResponse, (result as OkObjectResult)?.Value);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnNotFoundResult_WhenBookIsNotDeleted()
        {
            // Arrange
            int bookIdToDelete = 2;
            _bookServiceMock.Setup(s => s.DeleteBook(bookIdToDelete))!
                            .ReturnsAsync((BookResponseDto?)null);

            // Act
            var result = await _controller.DeleteBook(bookIdToDelete);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddBook_Should_ReturnBadRequest_WhenBookDtoIsNull()
        {
            // Arrange
            BookRequestDto? newBookDto = null;

            // Act
            var result = await _controller.AddBook(newBookDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(newBookDto, (result as BadRequestObjectResult)?.Value);
        }

        [Fact]
        public async Task AddBook_Should_ReturnOkResult_WhenBookIsAdded()
        {
            // Arrange
            var newBookDto = new BookRequestDto { Title = "New Book", Author = "Author", Publisher = "Publisher" };
            var addedBook = new BookResponseDto { Id = 1, Title = "New Book", Author = "Author", Publisher = "Publisher" };

            _bookServiceMock.Setup(s => s.AddBook(newBookDto))
                            .ReturnsAsync(addedBook);

            // Act
            var result = await _controller.AddBook(newBookDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(addedBook, (result as OkObjectResult)?.Value);
        }


    }
}
