using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Jorge",
                Password = "$2a$11$tBDwzGx2ogUlt826DdJ6ouWFwbeZdU.x.8tL1xGlIg7B/T4cNfQMG",
                Email = "jorgeProfe@gmail.com",
                Username = "jorginho777",
                CreatedAt = DateTime.Now
            }
           ) ;

            modelBuilder.Entity<Photo>().HasData(new Photo { 
                Id = 1,
                Label = "Cute Toph",
                Url = "https://64.media.tumblr.com/6aa69e26b42b3abf45d74a8ad0f6f51d/d29bec3c2925672b-28/s1280x1920/4557e3ef2a5cdbce39f431c06b34432b7f3a95f5.png",
                CreatedAt = DateTime.Now,
                UserId = 1
            });

            modelBuilder.Entity<Photo>().HasData(new Photo
            {
                Id = 2,
                Label = "Kurama",
                Url = "https://static.wikia.nocookie.net/ca0e929a-97d1-45ed-99e3-2280abbd961b",
                CreatedAt = DateTime.Now,
                UserId = 1
            });
        }
    }
}
