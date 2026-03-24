using AutoMapper;
using books_store_BLL.Dtos.Genre;
using books_store_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.MapperProfiles
{
    public class GenreMapperProfile: Profile
    {
        public GenreMapperProfile()
        {
            //GenreAntity => GenreDto
            CreateMap<GenreEntity, GenreDto>();

            //CreateGenreDto => GenreEntity
            CreateMap<CreateGenreDto, GenreEntity>();

            //UpdateGenreDto => GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>();

        }
    }
}
