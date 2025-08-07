using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public BlogPost? GetBlogPostByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Title.Equals(name));
        }
    }
}
