using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using P06Shop.Shared.Configuration;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.Services.MovieService;
using P11BlazorWebAssembly.Client;
using P11BlazorWebAssembly.Client.Services.CustomAuthStateProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
var appSettingsSection = appSettings.Get<AppSettings>();

var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl)
{
   // Path = appSettingsSection.BaseAPIUrl,
};
//Microsoft.Extensions.Http
builder.Services.AddHttpClient<IMovieService, MovieService>(client => client.BaseAddress = uriBuilder.Uri);
//builder.Services.Configure<AppSettings>(appSettings);
//builder.Services.AddSingleton<IOptions<AppSettings>>(new OptionsWrapper<AppSettings>(appSettingsSection));

builder.Services.AddSingleton(appSettingsSection);
builder.Services.AddBlazoredLocalStorage();

// autorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = uriBuilder.Uri);

await builder.Build().RunAsync();





//Microsoft.AspNetCore.Components.Authorization
//Blazored.LocalStorage