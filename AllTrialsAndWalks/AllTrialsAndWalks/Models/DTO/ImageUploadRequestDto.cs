using System.ComponentModel.DataAnnotations;

namespace AllTrialsAndWalks.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public String FileName { get; set; }
        public String? FileDescription { get; set; }
    }
}
