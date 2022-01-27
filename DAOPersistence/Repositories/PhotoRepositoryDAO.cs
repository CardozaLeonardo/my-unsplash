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
    public class PhotoRepositoryDAO: IPhotoRepository
    {
        private DAL.Photo _data;
        private IFactory<Photo> _factory;

        public PhotoRepositoryDAO()
        {
            _data = new DAL.Photo();
            _factory = new PhotoFactory();
        }

        public Photo Add(Photo model)
        {
            int id = _data.Add(model);
            model.Id = id;
            
            return model;
        }

        public void Delete(Photo entity)
        {
            throw new NotImplementedException();
        }

        public Photo Get(int id)
        {
            Photo photo = null;

            SqlDataReader reader = _data.GetById(id);
            if (reader.Read())
            {
                photo = new Photo();
                _factory.Initialize(photo, reader);
            }

            return photo;
        }

        public List<Photo> GetAll()
        {
            SqlDataReader reader = _data.GetAll();

            List<Photo> photos = new List<Photo>();
            while (reader.Read())
            {
                var photo = new Photo();
                _factory.Initialize(photo, reader);

                photos.Add(photo);
            }

            return photos;
        }

        public List<Photo> GetAllWithUser()
        {
            SqlDataReader reader = _data.GetAllWithUser();

            List<Photo> photos = new List<Photo>();
            while (reader.Read())
            {
                var photo = new Photo();
                _factory.InitializeWithRelations(photo, reader);

                photos.Add(photo);
            }

            return photos;
        }

        public Photo GetWithUser(int id)
        {
            Photo photo = null;

            SqlDataReader reader = _data.GetById(id);
            if (reader.Read())
            {
                photo = new Photo();
                _factory.InitializeWithRelations(photo, reader);
            }

            return photo;
        }

        public List<Photo> SearchByLabel(string labelTerm)
        {
            SqlDataReader reader = _data.SearchByLabel(labelTerm);

            List<Photo> photos = new List<Photo>();
            while (reader.Read())
            {
                var photo = new Photo();
                _factory.InitializeWithRelations(photo, reader);

                photos.Add(photo);
            }

            return photos;
        }

        public bool Update(int id, Photo model)
        {
            throw new NotImplementedException();
        }
    }
}
