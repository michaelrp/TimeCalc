using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimeCalc;
using TimeCalc.Services;

[assembly:AssemblyVersion("1.1.*")]

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ILocalStorage, LocalStorage>();
builder.Services.AddSingleton<ISolveCalculator, SolveCalculator>();

await builder.Build().RunAsync();
