using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Power.Weather.Test.Components.Contracts;
using Power.Weather.Test.Components.Services;

namespace Power.Weather.Test.Components;

public static class DependencyInjection
{
    public static IServiceCollection AddComponentsConfiguration(this IServiceCollection services, IConfigurationRoot config)
    {
        // adding services
        //services.AddScoped<IIdentityService, IdentityService>();
        services.AddTransient<IWeatherDataService, WeatherDataService>();

        return services;
    }
}