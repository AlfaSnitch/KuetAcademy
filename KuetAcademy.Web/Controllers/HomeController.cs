using KuetAcademy.Web.Models;
using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KuetAcademy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository,
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.courseRepository = courseRepository;
            this.teacherRepository = teacherRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var coursePosts = await courseRepository.GetAllAsync();
            var teacherPosts = await teacherRepository.GetAllAsync();

            BlogAndCourse obj = new BlogAndCourse();
            obj.BlogPosts = blogPosts;
            obj.Courses = coursePosts;
            obj.Teachers = teacherPosts;
            
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}