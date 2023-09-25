using KuetAcademy.Web.Data;
using KuetAcademy.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString"))); //injecting db context into applications

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("authDbConnectionString"))); //injecting authentication db context
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);

    options.SlidingExpiration = true;
});

builder.Services.AddDbContext<CourseDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CourseDbConnectionString")));

builder.Services.AddDbContext<teacherDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TeacherDbConnectionString")));

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();

builder.Services.AddScoped<IBlogPostCommentRespository, BlogPostCommentRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICourseRepository, CoursesRepository>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddScoped<ICoursePostCommentRepository, CoursePostCommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
