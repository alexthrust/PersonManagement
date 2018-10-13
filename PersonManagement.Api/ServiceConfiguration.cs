using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Api.Attributes;
using PersonManagement.Services.Abstraction;
using PersonManagement.Services.Implementation;

namespace PersonManagement.Api
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<ModelValidationAttribute>();

            services.AddTransient<IBaseApiService, BaseApiService>();
            services.AddTransient<IPersonService, PersonService>();
        }
    }
}