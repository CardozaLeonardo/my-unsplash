using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.DAL
{
    public class Photo: BaseManager
    {
        public SqlDataReader GetAll()
        {
            try
            {
                _data.OpenConnection();

                return _data.SelectRecords("GetAllPhotos");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Add(Domain.Entities.Photo entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Label", SqlDbType.VarChar),
                    new SqlParameter("@Url", SqlDbType.VarChar),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CreatedAt", SqlDbType.DateTime),
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                parameters[0].Value = entity.Label;
                parameters[1].Value = entity.Url;
                parameters[2].Value = entity.UserId;
                parameters[3].Value = entity.CreatedAt;

                parameters[4].Value = 0;
                parameters[4].Direction = ParameterDirection.Output;


                _data.OpenConnection();
                int value = _data.SaveRecordWithIdReturned("SavePhoto", parameters);
                _data.CloseConnection();

                return value;
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

                return _data.SelectRecords("GetPhotoById", parameters);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataReader GetAllWithUser()
        {
            try
            {

                _data.OpenConnection();

                return _data.SelectRecords("GetAllPhotosView");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public SqlDataReader SearchByLabel(string labelTerm)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Label", SqlDbType.VarChar)
                };
                parameters[0].Value = labelTerm;

                _data.OpenConnection();

                return _data.SelectRecords("SearchPhotoByLabel", parameters);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    
}
