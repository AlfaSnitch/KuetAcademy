namespace KuetAcademy.Web.Models.ViewModels
{
    public class AddCourseRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string VideoPath { get; set; }
        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public string Duration { get; set; }
    }
}
