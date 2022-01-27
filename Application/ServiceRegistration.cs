using Application.Repositories;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Services;
using Application.Services;
using System.Threading.Tasks;
using AutoMapper;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();*/

            services.AddScoped<IUploadImageService, CloudinaryImageUpload>();
            services.AddScoped<IHasingService, HashingService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
