using DemoApi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddNinjadog(config);

var app = builder.Build();

app.UseNinjadog();

app.Run();
