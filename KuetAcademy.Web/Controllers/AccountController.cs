using KuetAcademy.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetAcademy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,

            };

            var identityResult =  await userManager.CreateAsync(identityUser, registerViewModel.Password);
      
            if (identityResult.Succeeded)
            {
                //assign "user" role

                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if(roleIdentityResult.Succeeded)
                {
                    //show success notification
                    TempData["registerflag"] = "Successfully registered account.";
                    return RedirectToAction("Register");
                }

            }
            //show error notification
            TempData["notregister"] = " something went wrong!";
            return View();
        
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signinResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password,false,false);

            if (signinResult.Succeeded && signinResult!=null)
            {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl)) 
                {
                    TempData["LoginAlertMessage"] = "Login successful!";
                    return Redirect(loginViewModel.ReturnUrl);
                }
               
                return RedirectToAction("Index","Home");
            }

            //show errors
            ViewData["loginflag"] = "Invalid Username Or Password";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
     
}
