using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> usermanger;
        [BindProperty]
        public Register RegisterViewModel {  get; set; }
        public RegisterModel(UserManager<IdentityUser> usermanger)
        {
            this.usermanger = usermanger;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["Notifications"] = "Invalid form data. Please correct the errors.";
                    return Page();
                }

                if (RegisterViewModel == null)
                {
                    ViewData["Notifications"] = "Form data is missing. Please try again.";
                    return Page();
                }

                var newUser = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email
                };

                var identityResult = await usermanger.CreateAsync(newUser, RegisterViewModel.Password);
                if (identityResult.Succeeded)
                {
                    var addRolesResult = await usermanger.AddToRoleAsync(newUser, "User");

                    if (addRolesResult.Succeeded) 
                    {
                        ViewData["Notifications"] = "Successfully Registered";
                        return RedirectToPage("/Login");
                    }
                }
                else
                {
                    ViewData["Notifications"] = "Registration failed: " +
                        string.Join(", ", identityResult.Errors.Select(e => e.Description));
                }
            }
            catch (Exception ex)
            {
                ViewData["Notifications"] = "An error occurred: " + ex.Message;
            }

            return Page();
        }
    }
}
