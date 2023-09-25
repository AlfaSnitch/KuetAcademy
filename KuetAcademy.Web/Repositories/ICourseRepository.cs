using KuetAcademy.Web.Models.Domain;

namespace KuetAcademy.Web.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Courses>> GetAllAsync();

        Task<Courses?> GetByUrlHandleAsync(string urlHandle);

        Task<Courses?> GetAsync(Guid id);

        Task<Courses> AddAsync(Courses courses);

        Task<Courses?> UpdateAsync(Courses courses);

        Task<Courses?> DeleteAsync(Guid id);
    }
}
