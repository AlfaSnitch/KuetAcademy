﻿using KuetAcademy.Web.Data;
using KuetAcademy.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KuetAcademy.Web.Repositories
{
    public class TagRepository : ITagRepository // ctrl + . => implements the methods
    {
        private readonly BlogDBContext blogDBContext;

        public TagRepository(BlogDBContext blogDBContext)
        {
            this.blogDBContext = blogDBContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogDBContext.Tags.AddAsync(tag);
            await blogDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await blogDBContext.Tags.FindAsync(id);
            
            if (existingTag != null)
            {
                blogDBContext.Tags.Remove(existingTag);
                await blogDBContext.SaveChangesAsync(); 
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag?>> GetAllAsync()
        {
            return await blogDBContext.Tags.ToListAsync();
        }

        public  Task<Tag?> GetAsync(Guid id)
        {
            return blogDBContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogDBContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await blogDBContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
