using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDBContext blogDBContext;

        public BlogPostRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDBContext.BlogPosts.AddAsync(blogPost);
            await blogDBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await blogDBContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                blogDBContext.BlogPosts.Remove(existingBlog);
                await blogDBContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogDBContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await blogDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogDBContext.BlogPosts.Include(x=>x.Tags)
                .FirstOrDefaultAsync(x=>x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingTag = await blogDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=> x.Id == blogPost.Id);
            if(existingTag != null)
            {
                existingTag.Id = blogPost.Id;
                existingTag.Title = blogPost.Title;
                existingTag.Heading = blogPost.Heading;
                existingTag.Content = blogPost.Content;
                existingTag.ShortDescription = blogPost.ShortDescription;
                existingTag.Author = blogPost.Author;
                existingTag.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingTag.UrlHandle = blogPost.UrlHandle;
                existingTag.Visible = blogPost.Visible;
                existingTag.PublishedDate = blogPost.PublishedDate;
                existingTag.Tags = blogPost.Tags; 
                
                await blogDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
