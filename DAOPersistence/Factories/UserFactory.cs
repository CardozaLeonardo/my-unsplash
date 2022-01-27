using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.Factories
{
    public class UserFactory : IFactory<User>
    {
        public void Initialize(User entity, SqlDataReader reader)
        {
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.Email = reader["Email"].ToString();
            entity.Name = reader["Name"].ToString();
            entity.Password = reader["Password"].ToString();
            entity.PhotoUrl = reader["PhotoUrl"].ToString();
            entity.Username = reader["Username"].ToString();
            entity.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
        }

        public void InitializeWithRelations(User entity, SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
