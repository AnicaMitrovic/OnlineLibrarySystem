using AutoMapper;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Domain.Entities.Dtos.Response;

namespace OnlineLibrary.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookRequestDto, Book>();
            CreateMap<Book, BookResponseDto>();
        }
    }
}
