using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using BlogTalks.Domain.Shared;
using BlogTalks.Infrastructure.Data.DataContext;

namespace BlogTalks.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User? GetUsername(string username)
        {
            return _dbSet.Where(u => u.Username == username).FirstOrDefault();
        }

        public IEnumerable<User> GetUsersByIds(IEnumerable<int> ids)
        {
            return _context.Users.Where(u => ids.Contains(u.Id));
        }

        public User? Login(string username, string password)
        {
            var user = _dbSet.FirstOrDefault(u => username.Equals(username));

            var passwordVerified = PasswordHasher.VerifyPassword(password, user.Password);
            return user;
        }
        public User? Register(string username, string password, string name, string email)
        {
            var user = new User
            {
                Username = username,
                Password = PasswordHasher.HashPassword(password),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 5,
                Email = email,
            };
            _dbSet.Add(user);
            _context.SaveChanges();
            return user;
        }

    }
}
