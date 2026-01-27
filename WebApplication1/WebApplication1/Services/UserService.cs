using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly LibraryDbContext _dbContext;
        public UserService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }
        public bool Update(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return true;
        }
        public User? GetById(int id)
        {
            return _dbContext.Users.Find(id);
        }

    }
}
