using Microsoft.AspNetCore.Identity;

namespace KuetAcademy.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
