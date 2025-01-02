using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPost _blogPost;        
        public BlogPost BlogPost;
        public DetailsModel(IBlogPost blogPost)
        {
            _blogPost = blogPost;        
        }

        public async Task<IActionResult> OnGet(String urlHandle)
        {
            BlogPost = await _blogPost.GetAsync(urlHandle);

            return Page();
        }
    }
}
