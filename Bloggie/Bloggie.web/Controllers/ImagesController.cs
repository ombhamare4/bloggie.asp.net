using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImage _imageUpload;

        public ImagesController(IImage imageUpload)
        {
            _imageUpload = imageUpload;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            var result = await _imageUpload.UploadAync(file);
            if(result==null)
            {
                return Problem(
                            "An error occur. Image not uploaded.",
                            null,
                            (int)HttpStatusCode.InternalServerError);
            }

            return Ok(new { url = result });//200
        }
    }
}
