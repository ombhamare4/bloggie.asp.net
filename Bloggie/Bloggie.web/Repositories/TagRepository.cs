using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class TagRepository : ITags
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await bloggieDbContext.Tags.ToListAsync();          
            
            return tags.DistinctBy(x=> x.Name.ToLower());
        }
    }
}
