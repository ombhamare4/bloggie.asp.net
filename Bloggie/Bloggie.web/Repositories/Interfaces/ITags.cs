using Bloggie.web.Models.Domains;

namespace Bloggie.web.Repositories.Interfaces
{
    public interface ITags
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
