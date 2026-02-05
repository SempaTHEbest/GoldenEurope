using Microsoft.AspNetCore.Http;

namespace GoldenEurope.Core.Interfaces;

public interface IPhotoService
{
    Task<(string Url, string PublicId)> AddPhotoAsync(IFormFile file);
    Task DeletePhotoAsync(string publicId);
}