using DAOPersistence.Repositories;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence
{
    public static class ServiceRegistration
    {
        public static void AddDAOPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepositoryDAO>();
            services.AddScoped<IPhotoRepository, PhotoRepositoryDAO>();
        }
    }
}
