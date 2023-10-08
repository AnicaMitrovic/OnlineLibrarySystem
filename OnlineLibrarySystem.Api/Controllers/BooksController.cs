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

        //[HttpPost]
        //public async Task<IActionResult> AddBook([FromBody]BookRequestDto newBook)
        //{
        //    if (newBook == null)
        //    {
        //        return BadRequest(newBook);
        //    }

        //    return Ok(await _bookService.AddBook(newBook));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(int id)
        //{
        //    return Ok(await _bookService.DeleteBook(id));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBook(int id, [FromBody] Book bookToUpdate)
        //{
        //    return Ok(await _bookService.UpdateBook(id, bookToUpdate));
        //}
    }
}
