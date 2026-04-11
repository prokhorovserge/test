using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Services;
using System.Reflection;

namespace Power.Weather.Test.Components;

public static class DependencyInjection
{
    public static IServiceCollection AddComponentsConfiguration(this IServiceCollection services, IConfigurationRoot config)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(e => e.RegisterServicesFromAssemblies(assembly));

        // adding services
        //services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient<IWeatherDataService, WeatherDataService>();

        return services;
    }
}