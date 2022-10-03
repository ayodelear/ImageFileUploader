using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure;
using ImageFileUploaderAPI.Interfaces;
using ImageFileUploaderAPI.Models;

namespace ImageFileUploaderAPI.Services
{
    public class AzureStorage : IAzureStorage
    {
        private readonly string storageConnectionString;
        private readonly string storageContainerName;
        private readonly ILogger<AzureStorage> _logger;

        public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
        {
            storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }

        public async Task<BlobResponseDto> UploadAsync(string fileName, Stream stream)
        {
            BlobResponseDto response = new();
            var container = new BlobContainerClient(storageConnectionString, storageContainerName);

            try
            {
                var client = container.GetBlobClient(fileName);
                stream.Position = 0;
                // Upload the file async
                await client.UploadAsync(stream);

                response.Status = $"File {fileName} Uploaded Successfully";
                response.Blob = new BlobDto
                {
                    Uri = client.Uri.AbsoluteUri,
                    Name = client.Name,
                    StoreLocation = "Azure Cloud"
                };

            }
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {fileName} already exists in container. Set another name to store the file in the container: '{storageContainerName}.'");
                response.Status = $"File with name {fileName} already exists. Please use another name to store your file.";
                response.Error = true;
                response.IsDuplicated = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                // Log error to console and create a new response we can return to the requesting method
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            return response;
        }

    }
}
