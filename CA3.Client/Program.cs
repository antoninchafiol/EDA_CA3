using CA3.Client.Pages;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp =>
//    new HttpClient
//    {
//        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:5002")
//    });
builder.Services.AddScoped<DrugService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
