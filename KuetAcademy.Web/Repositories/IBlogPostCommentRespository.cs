using KuetAcademy.Web.Models.Domain;

namespace KuetAcademy.Web.Repositories
{
    public interface IBlogPostCommentRespository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId);
    }
}
