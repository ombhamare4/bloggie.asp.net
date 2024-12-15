using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPost blogPostRepository;

        public List<BlogPost> BlogPosts { get; set; }
        public ListModel(IBlogPost blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet()
        {
            BlogPosts = (await blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
