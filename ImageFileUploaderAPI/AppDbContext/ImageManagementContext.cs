using ImageFileUploaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageFileUploaderAPI.AppDbContext
{
    public class ImageManagementContext: DbContext
    {
        public ImageManagementContext(DbContextOptions<ImageManagementContext> options) : base(options)
        {
        }

        public DbSet<BlobDto> BlobDtos { get; set; }
    }
}
