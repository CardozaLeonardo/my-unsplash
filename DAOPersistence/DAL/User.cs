
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Domain.Entities;

namespace DAOPersistence.DAL
{
    public class User: BaseManager
    {
        public SqlDataReader GetAll()
        {
            try
            {
                _data.OpenConnection();

               return  _data.SelectRecords("GetAllUsers");
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int Add(Domain.Entities.User entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Name", SqlDbType.VarChar),
                    new SqlParameter("@Username", SqlDbType.VarChar),
                    new SqlParameter("@Email", SqlDbType.VarChar),
                    new SqlParameter("@Password", SqlDbType.VarChar),
                    new SqlParameter("@PhotoUrl", SqlDbType.VarChar),
                    new SqlParameter("@CreatedAt", SqlDbType.DateTime),
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                parameters[0].Value = entity.Name;
                parameters[1].Value = entity.Username;
                parameters[2].Value = entity.Email;
                parameters[3].Value = entity.Password;
                parameters[4].Value = entity.PhotoUrl;
                parameters[5].Value = entity.CreatedAt;

                parameters[6].Value = 0;
                parameters[6].Direction = ParameterDirection.Output;

                _data.OpenConnection();
                int res = _data.SaveRecordWithIdReturned("SaveUser", parameters);
                _data.CloseConnection();

                return res;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public SqlDataReader GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", SqlDbType.Int)
                };
                parameters[0].Value = id;

                _data.OpenConnection();

                return _data.SelectRecords("GetUserById", parameters);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataReader GetByUsernameOrEmail(string term)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Term", SqlDbType.VarChar)
                };
                parameters[0].Value = term;

                _data.OpenConnection();

                return _data.SelectRecords("GetUserByUsernameOrEmail", parameters);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
