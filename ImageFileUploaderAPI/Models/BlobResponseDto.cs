namespace ImageFileUploaderAPI.Models
{
    public class BlobResponseDto
    {
        public string? Status { get; set; }

        public bool Error { get; set; }

        public BlobDto Blob { get; set; }
        public bool IsDuplicated { get; internal set; }
        public bool IsOffLine { get; internal set; }
    }
}
