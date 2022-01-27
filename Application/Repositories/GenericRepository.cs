using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class GenericRepository<TEntity>  where TEntity : Base
    {
        protected Context _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public TEntity Add(TEntity model)
        {
            _dbSet.Add(model);
            _context.SaveChanges();
            return model;
        }

        public TEntity Get(int id)
        {
            TEntity entity;

            entity = _dbSet.Find(id);

            return entity;
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public bool Update(int id, TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!EntityExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }*/

                return false;
            }

            return true;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
