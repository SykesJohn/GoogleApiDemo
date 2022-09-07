using GoogleApiDemoWASM;
using GoogleApiDemoWASM.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GoogleApi.Extensions;

IConfiguration? config;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGoogleApiClientsNoHandler();
HttpClient myHost = new() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var res = await myHost.GetAsync("appsettings.json");
if(res.IsSuccessStatusCode)
	{
	config = new ConfigurationBuilder().AddJsonStream(res.Content.ReadAsStream()).Build();
	AutoComplete.GoogleApiKey=config["GoogleApiKey"];
#if DEBUG
	Console.WriteLine($"Google Map Key: {AutoComplete.GoogleApiKey}");
#endif //DEBUG
	}
await builder.Build().RunAsync();
