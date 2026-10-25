using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HomeClient;
using HomeClient.Services;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { // Singleton? Zakva ni meu skos nou client?
    BaseAddress = new Uri("http://localhost:5167/")
    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<NavigationManager>();

await builder
    .Build()
    .RunAsync();