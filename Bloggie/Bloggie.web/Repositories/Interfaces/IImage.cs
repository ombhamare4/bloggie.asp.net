namespace Bloggie.web.Repositories.Interfaces
{
    public interface IImage
    {
        Task<string> UploadAync(IFormFile file);
    }
}
