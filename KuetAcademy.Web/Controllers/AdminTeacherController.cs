using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KuetAcademy.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminTeacherController : Controller
    {
        private readonly ITeacherRepository teacherRepository;

        public AdminTeacherController(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherRequest addTeacherRequest)
        {
            var newTeacher = new Teachers
            {
                Name = addTeacherRequest.Name,
                ShortDescription = addTeacherRequest.ShortDescription,
                PreferredSubject = addTeacherRequest.PreferredSubject,
                ImageUrl = addTeacherRequest.ImageUrl,
                FacebookLink = addTeacherRequest.FacebookLink,
                TwitterLink = addTeacherRequest.TwitterLink,
                InstagramLink = addTeacherRequest.InstagramLink
            };
            await teacherRepository.AddAsync(newTeacher);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var allTeachers = await teacherRepository.GetAllAsync();
            return View(allTeachers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var newTeacher = await teacherRepository.GetAsync(id);

            if (newTeacher != null)
            {
                var model = new EditTeacherRequest
                {
                    Id = newTeacher.Id,
                    Name = newTeacher.Name,
                    ShortDescription = newTeacher.ShortDescription,
                    ImageUrl = newTeacher.ImageUrl,
                    PreferredSubject = newTeacher.PreferredSubject,
                    FacebookLink = newTeacher.FacebookLink,
                    TwitterLink = newTeacher.TwitterLink,
                    InstagramLink = newTeacher.InstagramLink
                };
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeacherRequest editTeacherRequest)
        {
            var teacherModel = new Teachers
            {
                Id = editTeacherRequest.Id,
                Name = editTeacherRequest.Name,
                ShortDescription = editTeacherRequest.ShortDescription,
                ImageUrl = editTeacherRequest.ImageUrl,
                PreferredSubject = editTeacherRequest.PreferredSubject,
                FacebookLink = editTeacherRequest.FacebookLink,
                InstagramLink = editTeacherRequest.InstagramLink,
                TwitterLink = editTeacherRequest.TwitterLink
            };

            var updateTeacher = await teacherRepository.UpdateAsync(teacherModel);

            if (updateTeacher != null)
            {
                //success
                return RedirectToAction("Edit");
            }
            //failure
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTeacherRequest editTeacherRequest)
        {
            var deletedTeacher = await teacherRepository.DeleteAsync(editTeacherRequest.Id);

            if (deletedTeacher != null)
            {
                //shor success
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTeacherRequest.Id });
        }


    }
}
