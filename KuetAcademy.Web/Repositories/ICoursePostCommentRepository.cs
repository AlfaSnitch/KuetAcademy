using KuetAcademy.Web.Models.Domain;

namespace KuetAcademy.Web.Repositories
{
    public interface ICoursePostCommentRepository
    {
        Task<CoursePostComment> AddAsync(CoursePostComment comment);
    }
}
