using ImageFileUploaderAPI.Models;

namespace ImageFileUploaderAPI.Repositories
{
    public interface IImageBlobRepository
    {
        int Update(BlobDto blobDto);
    }
}
