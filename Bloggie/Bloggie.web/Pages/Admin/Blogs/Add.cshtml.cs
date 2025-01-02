using System.Text.Json;
using Bloggie.web.Enums;
using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    [Authorize(Roles ="Admin")]
    public class AddModel : PageModel
    {
        private readonly IBlogPost blogPostRepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        [BindProperty]
        public String Tags { get; set; }
        public AddModel(IBlogPost blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var blogPost = new BlogPost()
                {
                    Heading = AddBlogPostRequest.Heading,
                    PageTitle = AddBlogPostRequest.PageTitle,
                    Content = AddBlogPostRequest.Content,
                    ShortDescription = AddBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                    URLHandle = AddBlogPostRequest.URLHandle,
                    PublishedDate = AddBlogPostRequest.PublishedDate,
                    Author = AddBlogPostRequest.Author,
                    Visible = AddBlogPostRequest.Visible,
                    Tags = new List<Tag>(Tags.Split(",").Select(x => new Tag() { Name = x.Trim() }))
                };

                await blogPostRepository.AddAsync(blogPost);

                Notification notification = new Notification
                {
                    Message = "Post Created Successfully",
                    NotificationType = NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
            }
            catch (Exception ex)
            {
                TempData["Notification"] = new Notification
                {
                    Message = "Something went wrong. Cannot Create Post",
                    NotificationType = NotificationType.Success
                };
            }

            return Redirect("/Admin/Blogs/List");
        }
    }
}
