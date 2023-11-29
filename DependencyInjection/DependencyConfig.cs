using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace DependencyInjection
{
    public static class DependencyConfig
    {
        public static IServiceCollection ConfigureDependencies(IServiceCollection Services)
        {
            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddTransient<IUnitOfWork,UnitOfWork>();
            Services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.Password.RequireDigit = true
                ).
                AddEntityFrameworkStores<ApplicationDbContext>();

            return Services;
        }
    }
}
