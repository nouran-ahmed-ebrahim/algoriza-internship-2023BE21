using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace DependencyInjection
{
    public static class DependencyConfig
    {
        public static ServiceProvider serviceProvider;
        public static IServiceCollection ConfigureDependencies(IServiceCollection Services)
        {
            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddTransient<IUnitOfWork,UnitOfWork>();

            // create server provider for SpecializationInitializer to use it
            serviceProvider = Services
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<SpecializationInitializer>()
            .BuildServiceProvider();

            
            return Services;
        }
    }
}
