using AutoMapper;
using books_store_BLL.Dtos.Genre;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(GenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            if (await _genreRepository.IsExistAsync(dto.Name))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Genre '{dto.Name}' has already exist"
                };
                
            }
            var entity = _mapper.Map<GenreEntity>(dto);
             var response = await _genreRepository.CreateAsync(entity);
            if (!response)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Something went wrong, the genre: '{dto.Name}' wasn't created"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"Genre '{dto.Name}' is created successfuly",
                Payload = _mapper.Map<GenreEntity>(entity)
            };
        }  // +
        public async Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            var entity = await _genreRepository.GetByIdAsync(dto.Id);
            if (entity == null) 
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the genre with id '{dto.Id}' isn't found!"
                };
            }
            if (await _genreRepository.IsExistAsync(dto.Name))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Genre '{dto.Name}' has already exist"
                };

            }
            var oldName = entity.Name;
           entity = _mapper.Map<UpdateGenreDto, GenreEntity>(dto, entity);
            var res = await _genreRepository.UpdateAsync(entity);
            if (!res)
            {
                return new ServiceResponse {
                    Success = false,
                    Message = "something went wrong, the genre isn't created"
                }; 
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the genre '{oldName}' is updated successfuly!"
            };

            
        }  // +
        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _genreRepository.Genres.ToListAsync();
            var dtos = _mapper.Map<List<GenreDto>>(entities);
            return new ServiceResponse
            {
                Success = true,
                Message = "genres are got successfuly",
                Payload = dtos
            };
        }   //+
        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the genre with id '{id}' isn't found"
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = $"The genre is got successfuly",
                    Payload = _mapper.Map<GenreDto>(entity)
                };
            }
        } //+


        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: the genre with id '{id}' isn't found"
                };
            }
            var res = await _genreRepository.DeleteAsync(id);
            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Something went wrong, the genre isn't deleted"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"The genre {entity.Name} is deleted"
            };
        } // 
        public async Task<ServiceResponse> GetByName(string name)
        {
            var response = await _genreRepository.GetByNameAsync(name);
            if (response == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"The genre '{name}' isn't found"

                };
            }
            var res = _mapper.Map<GenreDto>(response);
            return new ServiceResponse
            {
                Success = true,
                Message = "The genre is got successfuly",
                Payload = res
            };
        }   //+
    }
}
