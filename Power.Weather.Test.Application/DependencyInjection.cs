using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Power.Weather.Test.Application.Behaviors;
using Power.Weather.Test.Application.Events;
using System.Reflection;

namespace Power.Weather.Test.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfigurationRoot config)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(e => e.RegisterServicesFromAssemblies(assembly));
        // adding Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        services.AddScoped<IEventPipeline, EventPipeline>();
        //services.AddScoped<IIdentityService, IdentityService>();
        //services.AddHostedService<HostedService>();

        //services.AddTransient<IWeatherDataService, WeatherDataService>();

        return services;
    }
}