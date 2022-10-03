using ImageFileUploaderAPI.AppDbContext;
using ImageFileUploaderAPI.Models;

namespace ImageFileUploaderAPI.Repositories
{
    public class ImageBlobRepository : IImageBlobRepository
    {
        private readonly ImageManagementContext db;

        public ImageBlobRepository(ImageManagementContext db)
        {
            this.db = db;
        }

        public int Update(BlobDto blobDto)
        {
            db.BlobDtos.Add(blobDto);
            return db.SaveChanges();
        }
    }
}