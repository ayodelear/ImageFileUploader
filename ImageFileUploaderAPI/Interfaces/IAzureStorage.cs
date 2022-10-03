using ImageFileUploaderAPI.Models;

namespace ImageFileUploaderAPI.Interfaces
{
    public interface IAzureStorage
    {
        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> UploadAsync(string fileName, Stream stream);
    }
}
