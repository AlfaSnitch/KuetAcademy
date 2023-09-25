using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization; // for authorization
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

       
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        //[ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            //var name = addTagRequest.name;
            //var displayname = addTagRequest.displayName;

            // mapping AddTagRequest to Tag Domain model
            if (!ModelState.IsValid)
            {
                return View();
            }
            var tag = new Tag
            {
                Name = addTagRequest.name,
                DisplayName = addTagRequest.displayName
            };

            //await blogDBContext.Tags.AddAsync(tag);
            //await blogDBContext.SaveChangesAsync(); // to save the changes to the db
            TempData["AlertMessage"] = "New Tag Added successflly";
            await tagRepository.AddAsync(tag);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // use dbcontext to read data from database
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = blogDBContext.Tags.Find(id);

            //2nd method
            var tag = await tagRepository.GetAsync(id);  

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            
            if(updatedTag != null)
            {
                //show success
                TempData["UpdateAlertMessage"] = "New Tag updated successflly";
            }
            else
            {
                //show error notification   
            }
            //show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success notification
                TempData["AlertMessage"] = "New Tag deleted successflly";
                return RedirectToAction("List");
            }
            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
