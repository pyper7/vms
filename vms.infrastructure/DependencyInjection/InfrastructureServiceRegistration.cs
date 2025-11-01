using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.application.Interfaces.Repositories;
using vms.infrastructure.Repositories;

namespace vms.infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<VMSDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("VmsConnectionString")));


            services.AddScoped<IAccountRepository, AccountRepository>();


            return services;
        }
    }
}
