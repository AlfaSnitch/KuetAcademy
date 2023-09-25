using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuetAcademy.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            
            //map view model to domain model
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                Title = addBlogPostRequest.Title,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            //map tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.selectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //mapping tags back to domain model
            blogpost.Tags = selectedTags;
            
            await blogPostRepository.AddAsync(blogpost);
            TempData["addblog"] = " added a new blog";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repository
            var blogPosts = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //retrieve the result from repo
            var blogPosts = await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();
            //map the domain model into the view model
            if (blogPosts != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPosts.Id,
                    Title = blogPosts.Title,
                    PublishedDate = blogPosts.PublishedDate,
                    Content = blogPosts.Content,
                    Author = blogPosts.Author,
                    Heading = blogPosts.Heading,
                    FeaturedImageUrl = blogPosts.FeaturedImageUrl,
                    UrlHandle = blogPosts.UrlHandle,
                    ShortDescription = blogPosts.ShortDescription,
                    Visible = blogPosts.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    selectedTags = blogPosts.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                
                return View(model);
            }
            // pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map view model back to domain model

            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                Title = editBlogPostRequest.Title,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible,
            };
            // map tags into domain model
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.selectedTags) {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag!=null)
                    {
                         selectedTags.Add(foundTag);                       
                    }
                }
            }
            
            blogPostDomainModel.Tags = selectedTags;

            //submit information to repository update
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if(updatedBlog != null)
            {
                //show success notification
                TempData["update"] = " updated the blog";
                return RedirectToAction("Edit");
            }
            //show error
            TempData["notupdate"] = " something went wrong";
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            //talk to repo to delete blogpost and tags
            var deletedBlog =  await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlog != null)
            {
                // show success notification
                TempData["delete"] = " deleted the blog";
                return RedirectToAction("List");
            }

            //show error
            TempData["notdelete"] = " something went wrong";
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }

    }
}

