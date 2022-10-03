using ImageFileUploaderAPI.Models;
using System.Net.Http.Headers;

namespace ImageFileUploaderAPI.Interfaces
{
    public interface ILocalStorage
    {
        Task<BlobResponseDto> StoreAsync(IFormFile file);
    }
}
