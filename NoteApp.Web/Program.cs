using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NoteApp.Web;
using NoteApp.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<AppState>();

builder.Services.AddMudServices();

builder.Services.AddMediatR(x
    => x.RegisterServicesFromAssemblies(typeof(Configuration).Assembly));

builder.Services.AddHttpClient(Configuration.HttpClientName, options => {
    
    options.BaseAddress = new Uri(Configuration.ApiBaseUrl);
    
});

await builder.Build().RunAsync();
