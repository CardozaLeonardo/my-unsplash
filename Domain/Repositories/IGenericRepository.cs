using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Add(TEntity model);
        bool Update(int id, TEntity model);
        void Delete(TEntity entity);
    }
}
