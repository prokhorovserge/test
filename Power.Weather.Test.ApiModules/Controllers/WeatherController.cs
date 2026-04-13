using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Application.Handlers.GetCurrentWeather;
using Power.Weather.Test.Application.Handlers.GetWeatherForecast;
using System.Net.Mime;

namespace Power.Weather.Test.ApiModules.Controllers;

[ApiController]
[AllowAnonymous]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]/[action]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiExplorerSettings(GroupName = "weather")]
public class WeatherController : ControllerBase
{
    private readonly IEventPipeline _eventPipeline;

    public WeatherController(IEventPipeline eventPipeline)
    {
        _eventPipeline = eventPipeline;
    }

    [HttpGet]
    public async Task<ActionResult<GetCurrentWeatherResult>> GetCurrent([FromQuery] GetCurrentWeatherQuery query)
    {
        var result = await _eventPipeline.SendAsync(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<GetWeatherForecastResult>> GetForecast([FromQuery] GetWeatherForecastQuery query)
    {
        var result = await _eventPipeline.SendAsync(query);
        return Ok(result);
    }
}