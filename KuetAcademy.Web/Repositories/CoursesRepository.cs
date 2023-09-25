using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KuetAcademy.Web.Repositories
{
    public class CoursesRepository : ICourseRepository
    {
        private readonly CourseDbContext courseDbContext;

        public CoursesRepository(CourseDbContext courseDbContext)
        {
            this.courseDbContext = courseDbContext;
        }
        public async Task<Courses> AddAsync(Courses courses)
        {
            await courseDbContext.AddAsync(courses);
            await courseDbContext.SaveChangesAsync();
            return courses;
        }

        public async Task<Courses?> DeleteAsync(Guid id)
        {
            var existingCourse = await courseDbContext.Courses.FindAsync(id);
            
            if (existingCourse != null) 
            {
                courseDbContext.Courses.Remove(existingCourse);
                await courseDbContext.SaveChangesAsync();
                return existingCourse;
            }
            return null;
        }

        public async Task<IEnumerable<Courses>> GetAllAsync()
        {
            return await courseDbContext.Courses.ToListAsync();
        }

        public async Task<Courses?> GetAsync(Guid id)
        {
            return await courseDbContext.Courses.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Courses?> GetByUrlHandleAsync(string urlHandle)
        {
            return await courseDbContext.Courses.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Courses?> UpdateAsync(Courses courses)
        {
            var existingCourse =  await courseDbContext.Courses.FirstOrDefaultAsync(x => x.Id == courses.Id);

            if (existingCourse != null)
            {
                existingCourse.Id = courses.Id;
                existingCourse.Name = courses.Name;
                existingCourse.Description = courses.Description;
                existingCourse.Author = courses.Author;
                existingCourse.PublishedDate = courses.PublishedDate;
                existingCourse.FeaturedImageUrl = courses.FeaturedImageUrl;
                existingCourse.Duration = courses.Duration;
                existingCourse.VideoPath = courses.VideoPath;
                existingCourse.UrlHandle = courses.UrlHandle;
                existingCourse.Title = courses.Title;

                await courseDbContext.SaveChangesAsync();
                return existingCourse;
            }
            return null;
        }
    }
}
