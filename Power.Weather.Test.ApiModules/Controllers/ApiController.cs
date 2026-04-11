using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Power.Weather.Test.Application.Events;
using Power.Weather.Test.Components.Events.GetCurrentWeather;
using Power.Weather.Test.Components.Events.GetWeatherForecast;
using System.Net.Mime;

namespace Power.Weather.Test.ApiModules.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]/[action]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiExplorerSettings(GroupName = "weather")]
public class AccountsController : ControllerBase
{
    private readonly IEventPipeline _eventPipeline;

    public AccountsController(IEventPipeline eventPipeline)
    {
        _eventPipeline = eventPipeline;
    }

    [HttpGet]
    public async Task<ActionResult<GetCurrentWeatherQueryResult>> GetCurrentWeather([FromQuery] GetCurrentWeatherQuery query)
    {
        var result = await _eventPipeline.SendAsync(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GetWeatherForecastQueryResult>> GetWeatherForecast([FromQuery] GetWeatherForecastQuery query)
    {
        var result = await _eventPipeline.SendAsync(query);
        return Ok(result);
    }
}