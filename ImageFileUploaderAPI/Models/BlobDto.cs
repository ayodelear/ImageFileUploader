using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageFileUploaderAPI.Models
{
    public class BlobDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlobId { get; set; }

        [Required]
        public string Uri { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StoreLocation { get; set; }
    }
}
