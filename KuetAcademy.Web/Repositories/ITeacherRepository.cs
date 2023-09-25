using KuetAcademy.Web.Models.Domain;
using KuetAcademy.Web.Models.ViewModels;

namespace KuetAcademy.Web.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teachers>> GetAllAsync();

        Task<Teachers?> GetAsync(Guid id);

        Task<Teachers> AddAsync(Teachers teacher);

        Task<Teachers?> UpdateAsync(Teachers teacher);

        Task<Teachers?> DeleteAsync(Guid id);
    }
}
