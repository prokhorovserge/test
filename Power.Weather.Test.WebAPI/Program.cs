using Power.Weather.Test.ApiModules;
using Power.Weather.Test.Application;
using Power.Weather.Test.Application.Constants;
using Power.Weather.Test.Application.ModelBinding;
using Power.Weather.Test.Components;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    //.AddJsonFile($"appsettings.{environment}.json", false)
    .AddEnvironmentVariables();

var config = configurationBuilder.Build();

builder.Services.AddApiModulesConfiguration(config);

builder.Services
    .AddControllers(options =>
    {
        //options.Filters.Add<ApiExceptionFilterAttribute>();
        options.ModelBinderProviders.Insert(0, new KebabCaseEnumModelBinderProvider());
    })
    .AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient(HttpClientConstants.DisableAutoRedirect)
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AllowAutoRedirect = false // Disable automatic redirection
    });
builder.Services.AddHttpClient(HttpClientConstants.AllowAutoRedirect)
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AllowAutoRedirect = true // Enable automatic redirection
    });

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
