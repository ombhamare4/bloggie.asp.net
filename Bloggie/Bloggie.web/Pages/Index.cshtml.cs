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
        private readonly ITags _tags;
        public List<BlogPost> Blogs { get; set; }
        public List<Tag> Tags { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlogPost blogpost, ITags tags)
        {
            _logger = logger;
            _blogPost = blogpost;
            _tags = tags;
        }

        public async Task<IActionResult> OnGet()
        {
            Blogs = (await _blogPost.GetAllAsync()).ToList();
            Tags = (await _tags.GetAllAsync()).ToList();

            return Page();
        }
    }
}
