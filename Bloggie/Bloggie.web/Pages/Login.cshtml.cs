using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Bloggie.web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Login LoginViewModel { get; set; }
        public LoginModel(SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(String ReturnUrl) //ReturnUrl name should be same.
        {
            var user = await _userManager.FindByEmailAsync(LoginViewModel.Email);
            if(user == null)
            {
                ViewData["Notifications"] = "Invalid email or password";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                                    user.UserName,
                                    LoginViewModel.Password,
                                    false,
                                    lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToPage(ReturnUrl);
                }
                return RedirectToPage("Index");
            }

            ViewData["Notifications"] = "Invalid password";
            return Page();
        }
    }
}
