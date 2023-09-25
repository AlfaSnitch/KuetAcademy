namespace KuetAcademy.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        
        public string Heading { get; set; } // nullable korar jonno type er por ? dite hobe

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public string FeaturedImageUrl { get; set;}

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        //navigation
        public ICollection<Tag> Tags { get; set; } // 1 blog can have multiple blog posts

        public ICollection<BlogPostLike> Likes { get; set; }

        public ICollection<BlogPostComment> Comments { get; set; }
    }
}
