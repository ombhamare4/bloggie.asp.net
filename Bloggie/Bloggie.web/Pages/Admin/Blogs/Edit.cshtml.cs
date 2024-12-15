using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogPost blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public EditModel(IBlogPost blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostRepository.GetAsync(id);

        }
        public async Task<IActionResult> OnPostEdit() 
        {   
            await blogPostRepository.UpdateAsync(BlogPost);
            ViewData["MessageDescription"] = "Record Was Successfully Save";
            //return Redirect("/Admin/Blogs/List");
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();
        }
    }
}
