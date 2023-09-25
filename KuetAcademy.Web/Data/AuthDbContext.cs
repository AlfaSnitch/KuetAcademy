using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed the roles(user,admin and superadmin)
            var adminRoleId = "772f8a0a-e007-410f-adc3-602072b2f980";
            var superAdminRoleId = "4ea50518-7bf5-4338-963f-1ab6f8011a9a";
            var userRoleId = "538f41e5-644a-46d4-a9a3-c54d1c364eae";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                   Name = "SuperAdmin",
                   NormalizedName= "SuperAdmin",
                   Id = superAdminRoleId,
                   ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id =userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //seed superadmin user
            var superAdminId = "f7eefc6c-5e17-40b0-bfce-7741df55b264";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin",
                Email = "superadmin@kuetacademy.com",
                NormalizedEmail = "superadmin@kuetacademy.com".ToUpper(),
                NormalizedUserName = "superadmin".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //add all roles to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles); 

        }
    }
}
