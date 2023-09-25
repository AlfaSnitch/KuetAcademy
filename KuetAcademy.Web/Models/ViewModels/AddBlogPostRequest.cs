using KuetAcademy.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KuetAcademy.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        
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

        //collect tag
        public string[] selectedTags { get; set; } = Array.Empty<string>();

    }
}
