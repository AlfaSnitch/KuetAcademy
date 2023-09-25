using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetAcademy.Web.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICoursePostCommentRepository coursePostCommentRepository;

        public CourseController(ICourseRepository courseRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ICoursePostCommentRepository coursePostCommentRepository)
        {
            this.courseRepository = courseRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.coursePostCommentRepository = coursePostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var coursePost = await courseRepository.GetByUrlHandleAsync(urlHandle);
            var coursePostViewModel = new CourseDetailsViewModel();
            if (coursePost!=null)
            {
                coursePostViewModel = new CourseDetailsViewModel
                {
                    Id = coursePost.Id,
                    Name = coursePost.Name,
                    Author = coursePost.Author,
                    Description = coursePost.Description,
                    Title = coursePost.Title,
                    VideoPath = coursePost.VideoPath,
                    FeaturedImageUrl = coursePost.FeaturedImageUrl,
                    UrlHandle = coursePost.UrlHandle,
                    Duration = coursePost.Duration,
                    PublishedDate = coursePost.PublishedDate,
                };
            }

            return View(coursePostViewModel);
        }
    }
}
