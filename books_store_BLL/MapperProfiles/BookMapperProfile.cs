using AutoMapper;
using books_store_BLL.Dtos.Book;
using books_store_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.MapperProfiles
{
    public class BookMapperProfile: Profile
    {
        public BookMapperProfile()
        {
            // Bookentity => BookDto
            CreateMap<BookEntity, BookDto>();

            //CreateBookDto => DookEntity
            CreateMap<CreateBookDto, BookEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            //UpdateBookDto => DookEntity
            CreateMap<UpdateBookDto, BookEntity>();

        }

    }
}
