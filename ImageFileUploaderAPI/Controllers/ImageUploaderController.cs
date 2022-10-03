using Microsoft.AspNetCore.Mvc;
using Image = System.Drawing.Image;
using ImageFileUploaderAPI.Interfaces;
using ImageFileUploaderAPI.Repositories;
using ImageFileUploaderAPI.Models;

namespace ImageUploaderAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class ImageFileUploaderController : ControllerBase
    {
        private readonly ILogger<ImageFileUploaderController> _logger;
        private readonly IAzureStorage azureStorage;
        private readonly IImageBlobRepository imageBlobRepository;
        private readonly ILocalStorage localStorage;

        public ImageFileUploaderController(ILogger<ImageFileUploaderController> logger,
                                           IAzureStorage azureStorage,
                                           ILocalStorage localStorage,
                                           IImageBlobRepository imageBlobRepository)
        {
            _logger = logger;
            this.azureStorage = azureStorage;
            this.imageBlobRepository = imageBlobRepository;
            this.localStorage = localStorage;
        }

        [HttpPost(Name = "PostImageFileUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file == null || file.Length <= 0)
                    return BadRequest("A file was not provided.");

                if (file.ContentType.ToLower() != "image/jpg" &&
                    file.ContentType.ToLower() != "image/jpeg" &&
                    file.ContentType.ToLower() != "image/png")
                {
                    return BadRequest("File is a jpeg or png image");
                }

                BlobResponseDto blobContentInfo = new();
                using (var imageStream = file.OpenReadStream())
                {
                    using (var image = Image.FromStream(imageStream))
                    {
                        if (image == null || (
                            image.PhysicalDimension.Width != 1024 &&
                            image.PhysicalDimension.Height != 1024))
                        {
                            return BadRequest("Image size should be 1024*1024");
                        }

                        blobContentInfo = await azureStorage.UploadAsync(file.FileName, imageStream);
                        if (blobContentInfo.Error && !blobContentInfo.IsDuplicated)
                          blobContentInfo = await localStorage.StoreAsync(file);

                        if (blobContentInfo.Error)
                            return BadRequest(blobContentInfo.Status);

                        imageBlobRepository.Update(blobContentInfo.Blob);
                    }
                }

                return Ok(blobContentInfo.Blob);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}