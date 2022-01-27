using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.Factories
{
    public class PhotoFactory : IFactory<Photo>
    {
        public void Initialize(Photo entity, SqlDataReader reader)
        {
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.Label = reader["Label"].ToString();
            entity.Url = reader["Url"].ToString();
            entity.UserId = Convert.ToInt32(reader["UserId"]);
            entity.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
        }

        public void InitializeWithRelations(Photo entity, SqlDataReader reader)
        {
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.Label = reader["Label"].ToString();
            entity.Url = reader["Url"].ToString();
            entity.UserId = Convert.ToInt32(reader["UserId"]);
            entity.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

            entity.User = new User()
             {
                 Id = Convert.ToInt32(reader["UserId"]),
                 Email = reader["Email"].ToString(),
                 Name = reader["Name"].ToString(),
                 Username = reader["Username"].ToString(),
                 PhotoUrl = reader["PhotoUrl"].ToString(),
                 CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
             };
        }
    }
}
