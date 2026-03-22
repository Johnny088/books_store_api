using books_store_BLL.Dtos.Book;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly ImageService _imageService;
        public BookService(BookRepository bookRepository, ImageService imageService)
        {
            _bookRepository = bookRepository;
            _imageService = imageService;
        }
        public async Task<ServiceResponse> CreateAsync(CreateBookDto dto, string imagesPath)
        {
            var entity = new BookEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Rating = dto.Rating,
                Pages = dto.Pages,
                PublishedYear = dto.PublishedYear
            };

            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                ServiceResponse response = await _imageService.SaveAsync(dto.Image, imagesPath);
                if (!response.Success)
                {
                    return response;
                }
                entity.Image = response.Payload!.ToString()!;

            }

            bool result = await _bookRepository.CreateAsync(entity);

            if (!result)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "something went wrong, the book hasn't added"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the book '{entity.Title}' was added successfuly",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = dto.Title,
                    Description = dto.Description,
                    Image = entity.Image,
                    Rating = dto.Rating,
                    Pages = dto.Pages,
                    PublishedYear = dto.PublishedYear
                }
            };

        }//?????????
        public async Task<ServiceResponse> UpdateAsync(UpdateBookDto dto, string imagesPath)
        {
            var entity = await _bookRepository.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid Id: the book wasn't found"
                };
            }
            string oldTitle = entity.Title;
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            //entity.Image = dto.Image;
            entity.Rating = dto.Rating;
            entity.Pages = dto.Pages;
            entity.PublishedYear = dto.PublishedYear;

            if(dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string imagePath = Path.Combine(imagesPath, entity.Image);
                    var deleteResponse = _imageService.Delete(imagePath);
                    if (!deleteResponse.Success)
                    {
                        return deleteResponse;
                    }
                }
                var saveResponse = await _imageService.SaveAsync(dto.Image, imagesPath);
                if (!saveResponse.Success)
                {
                    return saveResponse;
                }
                entity.Image = saveResponse.Payload!.ToString();
            }

            var result = await _bookRepository.UpdateAsync(entity);
            if (!result)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Something went wrong, the book wasn't added"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the book '{oldTitle}' has updated successfuly"
            };
        }// ?????????
        public async Task<ServiceResponse> DeleteAsync(int id, string imagesPath)
        {
            var entity = await _bookRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the book wasn't found"
                };
            }
            if (entity.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                string imagePath = Path.Combine(imagesPath, entity.Image);
                ServiceResponse deleteResponse = _imageService.Delete(imagePath);
                if (!deleteResponse.Success)
                {
                    return deleteResponse;
                }
            }

            var response = await _bookRepository.DeleteAsync(id);
            if (!response)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "something went wrong, the book hasn't deleted"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the book '{entity.Title}' was deleted succesfully",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishedYear = entity.PublishedYear,

                }
            };
        }//???????????
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Invalid id: the book wasn't found"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the book '{entity.Title}' is got successfuly",
                Payload = new BookDto
                {
                    Id=entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishedYear = entity.PublishedYear,
                }
                
            };

        }//?????????????????
        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos =  await _bookRepository.Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Rating = b.Rating,
                    Pages = b.Pages,
                    PublishedYear = b.PublishedYear,
                }).ToListAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Books are got successfuly",
                Payload = dtos
               
            };
        } //+
        public async Task<ServiceResponse> GetByYearAsync(int year)
        {
            var response = await _bookRepository.getByYearAsync(year);
             var dtos = response.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Rating = b.Rating,
                    Pages = b.Pages,
                    PublishedYear = b.PublishedYear,
                }).ToList();
            if (dtos.Count == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"There isn't any published book from '{year}'"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Books are got successfuly",
                Payload = dtos
            };
            
        }//+
        public async Task<ServiceResponse> GetByRatingAsync(int rating)
        {
            var response = await _bookRepository.getByRatingAsync(rating);
            var dtos = response.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image,
                Rating = b.Rating,
                Pages = b.Pages,
                PublishedYear = b.PublishedYear,
            }).ToList();
            if (dtos.Count == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"There isn't any book with rating '{rating}'"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Books are got successfuly",
                Payload = dtos
            };
        } //+
        public async Task<ServiceResponse> GetByGenresAsync(string genre)
        {
            var response = await _bookRepository.GetByGenreAsync(genre);
            var dtos = response.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Image = b.Image,
                Rating = b.Rating,
                Pages = b.Pages,
                PublishedYear = b.PublishedYear,
            }).ToList();
            if (dtos.Count == 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"There isn't any book with the genre '{genre}'"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Books are got successfuly",
                Payload = dtos
            };

        }//+
    }
}
