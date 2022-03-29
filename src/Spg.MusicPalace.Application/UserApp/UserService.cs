using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;

namespace Spg.MusicPalace.Application
{
    public class UserService
    {
        private readonly MusicPalaceDbContext _dbContext;

        public UserService(MusicPalaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<User> ListAll()
        {
            return _dbContext.Users.ToList();
        }
    }
}