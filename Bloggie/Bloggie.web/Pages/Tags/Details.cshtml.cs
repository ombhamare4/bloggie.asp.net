using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPost _blogPost;

        [BindProperty]
        public List<BlogPost> BlogPosts { get; set; }
        public DetailsModel(IBlogPost blogPost)
        {
            _blogPost = blogPost;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            BlogPosts = (await _blogPost.GetAllAsync(tagName)).ToList();
            return Page();  
        }
    }
}
