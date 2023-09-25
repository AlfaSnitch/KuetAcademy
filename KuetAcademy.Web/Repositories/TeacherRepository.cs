using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly teacherDbContext teacherDbContext;

        public TeacherRepository(teacherDbContext teacherDbContext)
        {
            this.teacherDbContext = teacherDbContext;
        }

        public async Task<Teachers> AddAsync(Teachers teacher)
        {
            await teacherDbContext.AddAsync(teacher);
            await teacherDbContext.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teachers?> DeleteAsync(Guid id)
        {
            var existingTeacher = await teacherDbContext.Teachers.FindAsync(id);
            if (existingTeacher != null)
            {
                teacherDbContext.Teachers.Remove(existingTeacher);
                await teacherDbContext.SaveChangesAsync();
                return existingTeacher;
            }
            return null;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            return await teacherDbContext.Teachers.ToListAsync();
        }

        public async Task<Teachers?> GetAsync(Guid id)
        {
            return await teacherDbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Teachers?> UpdateAsync(Teachers teacher)
        {
            var existingTeacher = await teacherDbContext.Teachers.FirstOrDefaultAsync(x=>x.Id == teacher.Id);

            if (existingTeacher != null)
            {
                existingTeacher.Id = teacher.Id;
                existingTeacher.Name = teacher.Name;
                existingTeacher.ShortDescription = teacher.ShortDescription;
                existingTeacher.ImageUrl = teacher.ImageUrl;
                existingTeacher.PreferredSubject = teacher.PreferredSubject;
                existingTeacher.FacebookLink = teacher.FacebookLink;
                existingTeacher.InstagramLink = teacher.InstagramLink;
                existingTeacher.TwitterLink = teacher.TwitterLink;

                await teacherDbContext.SaveChangesAsync();
                return existingTeacher;
            }
            return null;
        }

    }
}
