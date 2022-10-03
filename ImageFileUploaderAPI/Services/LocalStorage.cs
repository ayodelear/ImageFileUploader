using ImageFileUploaderAPI.Interfaces;
using ImageFileUploaderAPI.Models;
using System.Net.Http.Headers;

namespace ImageFileUploaderAPI.Services
{
    public class LocalStorage : ILocalStorage
    {
        private readonly ILogger<AzureStorage> _logger;

        public LocalStorage(ILogger<AzureStorage> logger)
        {
            _logger = logger;
        }

        public async Task<BlobResponseDto> StoreAsync(IFormFile file)
        {
            BlobResponseDto response = new();

            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                response.Status = $"File {fileName} stored locally";
                response.Blob = new BlobDto
                {
                    Uri = fullPath,
                    Name = file.FileName,
                    StoreLocation = "Local"
                };
            }
            catch (IOException ex)
            {
                _logger.LogError($"File with name {file.FileName} was not stored");
                response.Status = $"File with name {file.FileName}  was not stored. Please try again.";
                response.Error = true;
            }

            return response;
        }

    }
}
