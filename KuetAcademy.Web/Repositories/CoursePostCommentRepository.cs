using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;

namespace KuetAcademy.Web.Repositories
{
    public class CoursePostCommentRepository : ICoursePostCommentRepository
    {
        private readonly CourseDbContext courseDbContext;

        public CoursePostCommentRepository(CourseDbContext courseDbContext)
        {
            this.courseDbContext = courseDbContext;
        }
        public async Task<CoursePostComment> AddAsync(CoursePostComment comment)
        {
            await courseDbContext.CoursePostComments.AddAsync(comment);
            await courseDbContext.SaveChangesAsync();
            return comment;
        }
    }
}
