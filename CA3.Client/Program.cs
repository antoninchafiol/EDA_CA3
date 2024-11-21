using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static CA3.Client.Pages.Drug;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


await builder.Build().RunAsync();
