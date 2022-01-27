using Domain.Entities;
using Domain.Repositories;
using Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Repositories
{
    public class PhotoRepository: GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(Context context): base(context)
        {
            _dbSet = context.Photos;
        }

        public List<Photo> GetAllWithUser()
        {
            return _dbSet.AsNoTracking().Include(p => p.User).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public Photo GetWithUser(int id)
        {
            return _dbSet.Include(p => p.User).FirstOrDefault(p => p.Id == id);
        }

        public List<Photo> SearchByLabel(string labelTerm)
        {
            return _dbSet.Where(p => p.Label.Contains(labelTerm)).Include(p => p.User).ToList();
        }
    }
}
