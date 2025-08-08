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
            return _dbSet.Where(u =>  u.Username == username).FirstOrDefault();
           // _context.Users.Where()
        }

        public User? Login(string username, string password)
        {
            var user = _dbSet.FirstOrDefault(u => username.Equals(username));
            if (user == null)
            {
                return null;
            }
            var passwordVerified = PasswordHasher.VerifyPassword(password, user.Password);
            if (!passwordVerified)
            {
                return null;
            }
            return user;
        }
        public User? Register(string username, string password, string name,string email)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
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
