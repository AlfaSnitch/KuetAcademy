namespace KuetAcademy.Web.Models.Domain
{
    public class BlogAndCourse
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Courses> Courses { get; set; }

        public IEnumerable<Teachers> Teachers { get; set; }
    }
}
