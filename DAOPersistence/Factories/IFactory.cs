using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.Factories
{
    public interface IFactory<TEntity>
    {
        public void Initialize(TEntity entity, SqlDataReader reader);
        public void InitializeWithRelations(TEntity entity, SqlDataReader reader);
    }
}
