using DemoLibrary;

namespace DemoApi.Domain;

[EnumGeneration]
public partial class WeatherForecast
{
    public static readonly WeatherForecast Today = new()
    {
        Date = DateTime.Today,
        Summary = "Sunny",
        TemperatureC = 32
    };
    
    public static readonly WeatherForecast Yesterday = new()
    {
        Date = DateTime.Today - TimeSpan.FromDays(1),
        Summary = "Very sunny",
        TemperatureC = 34
    };
    
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}