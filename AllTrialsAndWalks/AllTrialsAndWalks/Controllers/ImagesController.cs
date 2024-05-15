using AllTrialsAndWalks.Models.Domain;
using AllTrialsAndWalks.Models.DTO;
using AllTrialsAndWalks.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllTrialsAndWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) 
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageDto)
        {
            var fileExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (ModelState.IsValid)
            {
                if (fileExtension.Contains(Path.GetExtension(imageDto.File.FileName)))
                {
                    if (imageDto.File.Length < 1000000)
                    {
                        var image = new Image()
                        {
                            File = imageDto.File,
                            FileName = imageDto.FileName,
                            Description = imageDto.FileDescription
                        };

                        image = await imageRepository.UploadImage(image);
                        return Ok(image);
                    }
                }
            }
            return BadRequest("Unsupported file format");
        }
    }
}
