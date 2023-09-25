using KuetAcademy.Web.Models.Domain;

namespace KuetAcademy.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag); // also can be nullable return

        Task<Tag?> DeleteAsync(Guid id);
    }
}
