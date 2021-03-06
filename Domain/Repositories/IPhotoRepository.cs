using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPhotoRepository: IGenericRepository<Photo>
    {
        List<Photo> GetAllWithUser();
        Photo GetWithUser(int id);
        List<Photo> SearchByLabel(string labelTerm);
    }
}
