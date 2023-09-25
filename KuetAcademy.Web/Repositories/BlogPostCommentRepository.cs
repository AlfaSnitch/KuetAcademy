using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRespository
    {
        private readonly BlogDBContext blogDBContext;

        public BlogPostCommentRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogDBContext.BlogPostComment.AddAsync(blogPostComment);

            await blogDBContext.SaveChangesAsync();

            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId)
        {
            return await blogDBContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
