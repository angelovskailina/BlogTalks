using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Data.DataContext;

namespace BlogTalks.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public (int count, List<BlogPost> list) GetAllBlogposts(int pageNumber, int pageSize, string searchWord, string tag)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                query = query.Where(word =>
                word.Title.Contains(searchWord) ||
                word.Text.Contains(searchWord));
            }
            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(t => t.Title.Contains(tag));
            }

            var PageSize = pageSize <= 0 ? 10 : pageSize;
            var PageNumber = pageNumber <= 0 ? 1 : pageNumber;

            var count = query.Count();
            var pagedData = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return (count, pagedData);
        }
        public BlogPost? GetBlogPostByName(string name)
        {
            return _dbSet.FirstOrDefault(c => c.Title.Equals(name));
        }
    }
}
