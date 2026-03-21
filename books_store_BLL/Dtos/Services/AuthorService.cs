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

        public async Task<ServiceResponse> CreateAsync(CreateAuthorDto dto)
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
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Something went wrong, the author wasn't added"

                };
            }

            return new ServiceResponse
            {
                Success = true,
                Message = $"The author {entity.Name} was added successfuly",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                }
            };

        }
        public async Task<ServiceResponse> UpdateAsync(UpdateAuthorDto dto)
        {
            var entity = await _authorRepository.GetByIdAsync(dto.Id);
            if ( entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid id: The author wasn't found"
                };
            }
            string oldName = entity.Name;
            entity.Name = dto.Name;
            entity.BirthDate = dto.BirthDate;
            entity.Image = dto.Image;

            var result = await _authorRepository.UpdateAsync(entity);
            if (result) 
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = $"The Author {oldName} was updated successfuly"
                };
            }
            else 
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Something went wrong, the author wasn't updated"
                };
            }
            
        }
        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the author wasn't found"
                };
            }
            var result = await _authorRepository.DeleteAsync(entity);
            if (result)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = $"The author {entity.Name} was removed successfuly",
                    Payload = new AuthorDto
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        BirthDate = entity.BirthDate,
                        Image = entity.Image
                    }
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Something went wrong, Author wasn't removed"
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the author wasn't found"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "The author is got successfuly",
                Payload = new AuthorDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                }
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos = await _authorRepository.Authors
                .Select(a => new AuthorDto { Name = a.Name, BirthDate = a.BirthDate, Image = a.Image, Id = a.Id})
                .ToListAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Authors are got successfuly",
                Payload = dtos
            };
        }
        
    }
}
