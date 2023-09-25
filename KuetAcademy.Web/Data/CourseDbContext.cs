using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }

        public DbSet<CoursePostComment> CoursePostComments { get; set; }
    }
}
