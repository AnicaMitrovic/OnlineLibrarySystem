//using AutoMapper;
using AutoMapper;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;
using OnlineLibrary.Infrastructure.Interfaces;

namespace OnlineLibrary.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        //public async Task<ServiceResponse<List<Book>>> AddBook(BookRequestDto newBook)
        //{
        //    return await _bookRepository.AddBook(newBook);
        //}

        //public async Task<ServiceResponse<List<Book>>> DeleteBook(int id)
        //{
        //    return await _bookRepository.DeleteBook(id);
        //}

        public async Task<List<BookResponseDto>> GetAllBooks()
        {
            List<Book> books = await _bookRepository.GetAllBooks();           

            return books.Select(book => _mapper.Map<BookResponseDto>(book)).ToList();
        }

        public async Task<List<BookResponseDto>> SearchBooks(SearchBookRequestDto searchBookDto)
        {
            List<Book> books = await _bookRepository.SearchBooks(searchBookDto);

            return books.Select(book => _mapper.Map<BookResponseDto>(book)).ToList();
        }

        //public async Task<ServiceResponse<List<Book>>> UpdateBook(int id, Book bookToUpdate)
        //{
        //    return await _bookRepository.UpdateBook(id, bookToUpdate);
        //}
    }
}
