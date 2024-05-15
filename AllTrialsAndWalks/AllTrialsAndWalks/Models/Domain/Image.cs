using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllTrialsAndWalks.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? Description { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        [NotMapped]
        public  IFormFile File { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public string FileUrl { get; set; }
    }
}
