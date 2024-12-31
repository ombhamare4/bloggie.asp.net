using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPost _blogPost;
        public List<BlogPost> Blogs { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IBlogPost blogpost)
        {
            _logger = logger;
            _blogPost = blogpost;
        }

        public async Task<IActionResult> OnGet()
        {
            Blogs = (await _blogPost.GetAllAsync()).ToList();

            return Page();
        }
    }
}
