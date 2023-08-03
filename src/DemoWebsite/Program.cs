using DemoApi.Client;
using DemoWebsite;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped(sp => new ApiClient(
    "https://localhost:7006/",
    sp.GetRequiredService<HttpClient>()));

await builder
    .Build()
    .RunAsync()
    .ConfigureAwait(false);
