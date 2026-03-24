using AutoMapper;
using books_store_BLL.Dtos.Author;
using books_store_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.MapperProfiles
{
    public class AuthorMapperProfile: Profile
    {
        public AuthorMapperProfile()
        {
            //Author entity => dto
            CreateMap<AuthorEntity, AuthorDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country == null ? "Unknown" : src.Country));

            //CreateAuthorDto => AuthorEntity 
            CreateMap<CreateAuthorDto, AuthorEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
                

            //UpdateAuthorDto => AuthorEntity 
            CreateMap<UpdateAuthorDto, AuthorEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
