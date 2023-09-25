using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Data
{
    public class teacherDbContext : DbContext
    {
        public teacherDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Teachers> Teachers { get; set; }
    }
}
