using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class ImageService
    {
        
        public async Task<ServiceResponse> SaveAsync(IFormFile file, string dirPath)
        {
            try
            {
                var types = file.ContentType.Split("/");
                if(types.Length !=2 || (types[0] != "image"))
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Message = "Invalid type: given file isn't an image"
                    };
                }
                string imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string imagePath = Path.Combine(dirPath, imageName);

                using var fileStream = File.OpenWrite(imagePath);
                await file.CopyToAsync(fileStream);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "The image is added successfuly",
                    Payload = imageName
                };

            }
            catch
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Something went wrong, file wasn't saved"

                };
            }
        }
        public ServiceResponse Delete(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "The image was removed successfuly"
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = $"The image {imagePath} wasn't found"
            };
        }
    
    }
}
