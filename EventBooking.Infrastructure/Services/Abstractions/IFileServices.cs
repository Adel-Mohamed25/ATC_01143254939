using Microsoft.AspNetCore.Http;

namespace EventBooking.Infrastructure.Services.Abstractions
{
    public interface IFileServices
    {
        Task<string> UploadImageAsync(string location, IFormFile file);
        Task<bool> DeleteImageAsync(string location, string imageUrl);
    }
}
