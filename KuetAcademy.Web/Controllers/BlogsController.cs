using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KuetAcademy.Web.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRespository blogPostCommentRespository;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRespository blogPostCommentRespository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRespository = blogPostCommentRespository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPostLikeViewModel = new BlogDetailsViewModel();
            var blogpost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            if (blogpost != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogpost.Id);

                if (signInManager.IsSignedIn(User))
                {
                    //get like for this blog for this user
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogpost.Id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    { 
                        var likefromuser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likefromuser != null;
                    }
                }

                //get comments for blog posts
                var blogCommentsDomainModel = await blogPostCommentRespository.GetCommentByBlogIdAsync(blogpost.Id);

                var blogCommentsForView = new List<BlogComment>();

                foreach (var comment in blogCommentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }

                blogPostLikeViewModel = new BlogDetailsViewModel
                {
                    Id = blogpost.Id,
                    Content = blogpost.Content,
                    Title = blogpost.Title,
                    Author = blogpost.Author,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    Heading = blogpost.Heading,
                    PublishedDate = blogpost.PublishedDate,
                    ShortDescription = blogpost.ShortDescription,
                    UrlHandle = blogpost.UrlHandle,
                    Visible = blogpost.Visible,
                    Tags = blogpost.Tags,

                    TotalLikes = totalLikes,

                    Liked = liked,

                    Comments = blogCommentsForView
                };
            }

            return View(blogPostLikeViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };


                await blogPostCommentRespository.AddAsync(domainModel);
                return RedirectToAction("Index", "Blogs",
                    new { urlHandle = blogDetailsViewModel.UrlHandle });
            }
            return View();
        }

    }
}
