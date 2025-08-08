using BlogTalks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogTalks.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
