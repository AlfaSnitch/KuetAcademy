using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuetAcademy.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }

        public string Heading { get; set; } // nullable korar jonno type er por ? dite hobe

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        //display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        //collect tags
        public string[] selectedTags { get; set; } = Array.Empty<string>();
    }
}
