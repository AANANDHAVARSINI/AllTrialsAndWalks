using AllTrialsAndWalks.Data;
using AllTrialsAndWalks.Models.Domain;

namespace AllTrialsAndWalks.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly AllTrialsDbcontext dbcontext;
        private readonly IWebHostEnvironment webHost;

        public ImageRepository(AllTrialsDbcontext dbcontext, IWebHostEnvironment webHost) 
        {
            this.dbcontext = dbcontext;
            this.webHost = webHost;
        }
        public async Task<Image> UploadImage(Image image)
        {
            var currentPath = Path.Combine(webHost.ContentRootPath, "Images",$"{image.FileName}.{ Path.GetExtension(image.File.FileName)}");

            image.FileSize = image.File.Length;
            image.FileUrl = currentPath;
            image.FileExtension = Path.GetExtension(image.File.FileName);

            using (var stream = File.Create(currentPath))
            {
               await image.File.CopyToAsync(stream);
            }

            await dbcontext.Images.AddAsync(image);
            return image;
        }
    }
}
