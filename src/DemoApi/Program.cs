using DemoApi;

class Program
{
    public static void Main(string [] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var config = builder.Configuration;

        NinjadogExtensions.AddNinjadog(services, config);

        var app = builder.Build();

        NinjadogExtensions.UseNinjadog(app);

        app.Run();
    }
}
