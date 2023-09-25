using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace KuetAcademy.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminCourseController : Controller
    {
        private readonly ICourseRepository courseRepository;

        public AdminCourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseRequest course)
        {
            var newCourse = new Courses
            {
                Title = course.Title,
                Name = course.Name,
                Description = course.Description,
                FeaturedImageUrl = course.FeaturedImageUrl,
                UrlHandle = course.UrlHandle,
                PublishedDate = course.PublishedDate,
                Author = course.Author,
                Duration = course.Duration,
                VideoPath = course.VideoPath      
            };


            await courseRepository.AddAsync(newCourse);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var allCourses =  await courseRepository.GetAllAsync();
            return View(allCourses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var newCourse = await courseRepository.GetAsync(Id);

            if(newCourse!=null)
            {
                var model = new EditCourseRequest
                {
                    Id = newCourse.Id,
                    Title = newCourse.Title,
                    Name = newCourse.Name,
                    Description = newCourse.Description,
                    PublishedDate = newCourse.PublishedDate,
                    Author = newCourse.Author,
                    Duration = newCourse.Duration,
                    VideoPath = newCourse.VideoPath,
                    UrlHandle = newCourse.UrlHandle,
                    FeaturedImageUrl = newCourse.FeaturedImageUrl
                };

                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseRequest editCourseRequest)
        {
            var courseModel = new Courses
            {
                Id=editCourseRequest.Id,
                Title=editCourseRequest.Title,
                Name=editCourseRequest.Name,
                Description=editCourseRequest.Description,
                PublishedDate=editCourseRequest.PublishedDate,
                FeaturedImageUrl=editCourseRequest.FeaturedImageUrl,
                UrlHandle=editCourseRequest.UrlHandle,
                Author = editCourseRequest.Author,
                Duration = editCourseRequest.Duration,
                VideoPath = editCourseRequest.VideoPath
            };

            var updatedCourse = await courseRepository.UpdateAsync(courseModel);

            if(updatedCourse != null)
            {
                //show success notification
                return RedirectToAction("Edit");
            }
            //show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditCourseRequest editCourseRequest)
        {
            var deletedCourse = await courseRepository.DeleteAsync(editCourseRequest.Id);

            if (deletedCourse !=null)
            {
                //show succes
                return RedirectToAction("List");    
            }
            //show error
            return RedirectToAction("Edit", new { id = editCourseRequest.Id });
        }

    }
}
