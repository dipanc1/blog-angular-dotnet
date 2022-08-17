using Microsoft.EntityFrameworkCore;
using morphBlog.API.Models.Entities;

namespace MorphBlog.API.Data
{
    public class MorphBlogDbContext : DbContext
    {
        public MorphBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        //DBSet
        public DbSet<Post> Posts { get; set; }
    }
}
