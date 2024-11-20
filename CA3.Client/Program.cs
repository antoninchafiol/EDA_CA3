using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static CA3.Client.Pages.Drug;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://api.fda.gov/") });
builder.Services.AddScoped<OpenFdaService>();


await builder.Build().RunAsync();
