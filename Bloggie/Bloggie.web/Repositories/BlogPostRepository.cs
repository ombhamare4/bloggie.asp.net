
using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPost
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlogPost = bloggieDbContext.BlogPosts.Find(id);
            if (existingBlogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await bloggieDbContext.SaveChangesAsync();
                return(true);
            }
            return (false);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts
                                    .Include(nameof(BlogPost.Tags))
                                    .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
        {
            return await bloggieDbContext.BlogPosts
                                    .Include(nameof(BlogPost.Tags))
                                    .Where(x=>x.Tags.Any(x=>x.Name == tagName))
                                    .ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {   
            return await bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
                                                    .FirstOrDefaultAsync(x=>x.Id ==id);
        }

        public async Task<BlogPost> GetAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts
                                    .Include(nameof(BlogPost.Tags))
                                    .FirstOrDefaultAsync(x => x.URLHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await bloggieDbContext.BlogPosts
                                                   .Include(nameof(BlogPost.Tags))
                                                   .FirstOrDefaultAsync(x => x.Id == blogPost.Id); ;

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.URLHandle = blogPost.URLHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
            }
            //Delete the exisiting tags
            if (blogPost.Tags != null && blogPost.Tags.Any()) 
            {
                bloggieDbContext.RemoveRange(existingBlogPost.Tags);

                //Add new tags
                blogPost.Tags.ToList().ForEach(x=> x.BlogPostId = blogPost.Id);
                bloggieDbContext.Tags.AddRangeAsync(blogPost.Tags);
            }
            //Save Changes
            await bloggieDbContext.SaveChangesAsync();
            return (existingBlogPost);
        }
    }
}
