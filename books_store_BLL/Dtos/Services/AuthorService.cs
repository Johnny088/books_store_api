using books_store_BLL.Dtos.Author;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;
        private readonly ImageService _imageService;

        public AuthorService(AuthorRepository authorRepository, ImageService imageService)
        {
            _authorRepository = authorRepository;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateAuthorDto dto, string imagesPath)
        {
            var entity = new AuthorEntity
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
            };
            if(dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
              ServiceResponse response = await _imageService.SaveAsync(dto.Image, imagesPath);
              if(!response.Success)
                {
                    return response;
                }
                entity.Image = response.Payload!.ToString()!;
            }
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
        public async Task<ServiceResponse> UpdateAsync(UpdateAuthorDto dto, string imagesPath)
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
            


            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                if(!string.IsNullOrEmpty(entity.Image))
                {
                    string imagePath = Path.Combine(imagesPath, entity.Image);
                    var deleteResponse = _imageService.Delete(imagePath);
                    if (!deleteResponse.Success)
                    {
                        return deleteResponse;
                    }

                }
                ServiceResponse saveResponse = await _imageService.SaveAsync(dto.Image, imagesPath);
                if (!saveResponse.Success)
                {
                    return saveResponse;
                }
                entity.Image = saveResponse.Payload!.ToString();
            }

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
        public async Task<ServiceResponse> DeleteAsync(int id, string imagesPath)
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

            if (!string.IsNullOrEmpty(imagesPath))
            {
                string imagePath = Path.Combine(imagesPath, entity.Image);
                var response = _imageService.Delete(imagePath);
                if (!response.Success)
                {
                    return response;
                }
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
                .Select(a => new AuthorDto { Name = a.Name, BirthDate = a.BirthDate,Image = a.Image, Id = a.Id})
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
