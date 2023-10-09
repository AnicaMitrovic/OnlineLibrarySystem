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
        public async Task<IActionResult> SearchBooks([FromBody] SearchBookRequestDto searchBookDto)
        {
            return Ok(await _bookService.SearchBooks(searchBookDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto newBookDto)
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
            var responseId = await _bookService.DeleteBook(id);

            if (responseId is null)
            {
                return NotFound(responseId);
            }

            return Ok(responseId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequestDto bookToUpdateDto, int id)
        {
            if (bookToUpdateDto is null)
            {
                return BadRequest(bookToUpdateDto);
            }

            return Ok(await _bookService.UpdateBook(bookToUpdateDto, id));
        }
    }
}
