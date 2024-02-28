using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 默认配置精度2
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Model Get2()
    {
        return new Model();
    }
    /// <summary>
    /// 精度4
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [JsonSettingsName("level4")]
    public Model Get4()
    {
        return new Model();
    }
    /// <summary>
    /// 精度6
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [JsonSettingsName("level6")]

    public Model Get6()
    {
        return new Model();
    }

    public class Model
    {
        public double A { get; set; } = 1.11111111111222;
    }
}