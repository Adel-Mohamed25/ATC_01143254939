using EventBooking.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EventBooking.Infrastructure.Services.implementation
{
    public class FileServices : IFileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileServices> _logger;

        public FileServices(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FileServices> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        /// <summary>
        /// Uploads an image to the specified location and returns its URL.
        /// </summary>
        /// <param name="location">The directory where the image should be saved.</param>
        /// <param name="file">The image file to be uploaded.</param>
        /// <returns>
        /// A string representing the full URL of the uploaded image if successful, 
        /// or an error message in case of failure.
        /// </returns>
        public async Task<string> UploadImageAsync(string location, IFormFile file)
        {
            try
            {
                if (string.IsNullOrEmpty(location))
                    throw new ArgumentException("Invalid location", nameof(location));
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty or null", nameof(file));

                var path = Path.Combine(_webHostEnvironment.WebRootPath, location);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(extension))
                    throw new ArgumentException("Invalid file extension", nameof(file));

                var fileName = $"{Guid.NewGuid():N}{extension}";
                var fileFullPath = Path.Combine(path, fileName);

                await using (var fileStream = new FileStream(fileFullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }

                var request = _httpContextAccessor.HttpContext?.Request;
                if (request == null)
                    throw new InvalidOperationException("Cannot determine request context.");

                var baseUrl = $"{request.Scheme}://{request.Host}";
                var relativePath = $"/{location}/{fileName}".Replace("//", "/");
                return $"{baseUrl}{relativePath}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload image");
                return null!;
            }
        }

        /// <summary>
        /// Deletes an image from the server.
        /// </summary>
        /// <param name="imageUrl">The URL or relative path of the image to be deleted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.  
        /// Returns a boolean indicating whether the deletion was successful.
        /// </returns>
        public async Task<bool> DeleteImageAsync(string location, string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return await Task.FromResult(false);
            }

            try
            {
                var fileName = Path.GetFileName(imageUrl);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, location, fileName);

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete image.");
                return false;
            }
        }

    }
}
