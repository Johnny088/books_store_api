using books_store_BLL.Dtos.Author;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDto?> CreateAsync(CreateAuthorDto dto)
        {
            var entity = new AuthorEntity
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Image = dto.Image,
            };
            bool result = await _authorRepository.CreateRangeAsync(entity);
            if (!result)
            {
                return null;
            }
            return new AuthorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.BirthDate,
                Image = entity.Image,
            };
        }
    }
}
