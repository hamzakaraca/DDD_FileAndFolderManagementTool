using Application.Abstract;
using DataAccess.Abstract;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFolderServices(this IServiceCollection services)
        {
            services.AddScoped<IFolderLoadService, FolderLoadService>();
            services.AddScoped<IFolderDeleteService, FolderDeleteService>();
            services.AddScoped<IFolderCreateService, FolderCreateService>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IFolderSearchService, FolderSearchService>();

            return services;
        }
    }

}
