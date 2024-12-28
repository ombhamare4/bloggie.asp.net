using Bloggie.web.Repositories.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.web.Repositories
{
    public class ImageRepository : IImage
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        private readonly Account _account;

        public ImageRepository(IConfiguration config)
        {
            _config = config;
            _account = new Account(_config.GetSection("CloudinarySettings")["CloudName"],
                                    _config.GetSection("CloudinarySettings")["APIKey"],
                                    _config.GetSection("CloudinarySettings")["APIKeySecret"]);
            _cloudinary = new Cloudinary(_account);
        }
        public async Task<string> UploadAync(IFormFile file)
        {

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(400).Width(800)
                                    .Crop("fill").Gravity("face"),
                    Folder = "bloggie-net8",
                    DisplayName = file.FileName,
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK) 
                { 
                    return (uploadResult.SecureUrl.ToString()); 
                }
            }

            return null;
        }
    }
}
