using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Power.Weather.Test.Application;
using Power.Weather.Test.Components.Models;
using System.Reflection;

namespace Power.Weather.Test.ApiModules;

public static class DependencyInjection
{
    public static IServiceCollection AddApiModulesConfiguration(this IServiceCollection services, IConfigurationRoot config)
    {
        BindConfigOptions(services, config);
        services.AddApplicationConfiguration(config);
        return services;
    }

    public static IMvcBuilder AddControllers(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly()));
        return mvcBuilder;
    }

    private static void BindConfigOptions(IServiceCollection services, IConfigurationRoot config)
    {
        //services.Configure<GeneralOptions>(opt => config.GetSection("General").Bind(opt));
        services.Configure<IntegrationConfig>(opt => config.GetSection("Integrations").Bind(opt));
        //services.Configure<TokenProviderOptions>(opt => config.GetSection("TokenProviderOptions").Bind(opt));
        //services.Configure<ConnectionStrings>(opt => config.GetSection("ConnectionStrings").Bind(opt));
    }
}