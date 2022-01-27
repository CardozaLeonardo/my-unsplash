using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>();
            CreateMap<User, ReadUser>();
            CreateMap<UpdateUser, User>();
            CreateMap<User, UpdateUser>();
        }
    }
}
