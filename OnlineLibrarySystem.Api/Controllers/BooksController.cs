using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.Entities.Dtos.Request;

namespace OnlineLibrarySystem.Api.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooks());
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchBooks(SearchBookRequestDto searchBookDto)
        {
            return Ok(await _bookService.SearchBooks(searchBookDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookRequestDto newBookDto)
        {
            if (newBookDto is null)
            {
                return BadRequest(newBookDto);
            }

            return Ok(await _bookService.AddBook(newBookDto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _bookService.DeleteBook(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequestDto bookToUpdateDto, int id)
        {
            return Ok(await _bookService.UpdateBook(bookToUpdateDto, id));
        }
    }
}
