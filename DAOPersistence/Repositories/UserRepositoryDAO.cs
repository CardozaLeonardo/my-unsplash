using DAOPersistence.Factories;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.Repositories
{
    public class UserRepositoryDAO : IUserRepository
    {
        private DAL.User _data;
        private IFactory<User> _factory;

        public UserRepositoryDAO()
        {
            _data = new DAL.User();
            _factory = new UserFactory();
        }
        
        public User Add(User model)
        {
            try
            {
                int id = _data.Add(model);
                model.Id = id;
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            try
            {
                User user = null;

                SqlDataReader reader = _data.GetById(id);

                if (reader.Read())
                {
                    user = new User();
                    _factory.Initialize(user, reader);
                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                List<User> users = new List<User>();

                SqlDataReader reader = _data.GetAll();

                while (reader.Read())
                {
                    var user = new User();
                    _factory.Initialize(user, reader);

                    users.Add(user);
                }

                return users;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetByEmailOrUsername(string term)
        {
            try
            {
                User user = null;

                SqlDataReader reader = _data.GetByUsernameOrEmail(term);

                if (reader.Read())
                {
                    user = new User();
                    _factory.Initialize(user, reader);
                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(int id, User model)
        {
            throw new NotImplementedException();
        }
    }
}
