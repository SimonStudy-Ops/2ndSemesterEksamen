using BasementClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;   

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient til server API
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5299") 
});

// ⬅️ Tilføj LocalStorage
builder.Services.AddBlazoredLocalStorage();

// ⬅️ AuthService bruger LocalStorage
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();