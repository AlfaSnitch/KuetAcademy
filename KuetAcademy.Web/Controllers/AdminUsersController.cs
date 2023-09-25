using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetAcademy.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository,
            UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var userViewModel = new UserViewModel();
            userViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                userViewModel.Users.Add(new Models.ViewModels.User()
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser,request.Password);
            
            if(identityResult is not null) 
            {
                if(identityResult.Succeeded)
                {
                    //assign roles
                    var roles = new List<string> { "User" };

                    if (request.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }
                    identityResult = await userManager.AddToRolesAsync(identityUser, roles); 

                    if(identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if(user != null)
            {
                var i = await userManager.DeleteAsync(user);

                if(i is not null && i.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            // not delete notification
            return View();
        }
    }
}
