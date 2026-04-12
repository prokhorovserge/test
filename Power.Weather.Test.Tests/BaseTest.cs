using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Power.Weather.Test.Application;

namespace Power.Weather.Test.Tests;

public abstract class BaseTest
{
    private static readonly IServiceCollection _serviceCollection;

    static BaseTest()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddApplicationConfiguration(config);
    }
}
