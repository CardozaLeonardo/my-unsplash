
using Application.Actions.PhotoActions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class PhotoProfile: Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, ReadPhoto>();
            CreateMap<CreatePhoto, Photo>();
            CreateMap<Photo, ReadPhotoWithUser>();
        }
    }
}
