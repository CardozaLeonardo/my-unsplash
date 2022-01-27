using Domain.Entities;
using Domain.Repositories;
using Persistence;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context context): base(context)
        {
            _dbSet = context.Users;
        }

        public User GetByEmailOrUsername(string term)
        {
            return _dbSet.FirstOrDefault(p => p.Username == term || p.Email == term);
        }

    }
}
