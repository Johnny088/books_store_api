using books_store_BLL.Dtos.Author;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            bool result = await _authorRepository.CreateAsync(entity);
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
        public async Task<AuthorDto?> UpdateAsync(UpdateAuthorDto dto)
        {
            var entity = await _authorRepository.GetByIdAsync(dto.Id);
            if ( entity == null)
            {
                return null;
            }

            entity.Name = dto.Name;
            entity.BirthDate = dto.BirthDate;
            entity.Image = dto.Image;

            var result = await _authorRepository.UpdateAsync(entity);
            if (result) 
            {
                return new AuthorDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                };
            }
            return null;
        }
        public async Task<AuthorDto?> DeleteAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var result = await _authorRepository.DeleteAsync(entity);
            if (result)
            {
                return new AuthorDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                };
            }
            return null;
        }
        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);
            if (entity == null)
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

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var dtos = await _authorRepository.Authors
                .Select(a => new AuthorDto { Name = a.Name, BirthDate = a.BirthDate, Image = a.Image, Id = a.Id})
                .ToListAsync();
            return dtos;
        }
    }
}
