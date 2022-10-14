using DemoApi;
using DemoApi.Database;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7270");
        });
});

services.AddNinjadog(config);

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);
app.UseNinjadog();

await app.Services
    .GetRequiredService<DatabaseInitializer>()
    .InitializeAsync()
    .ConfigureAwait(false);

app.Run();
