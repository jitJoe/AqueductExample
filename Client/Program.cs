using System;
using System.Net.Http;
using System.Threading.Tasks;
using AqueductExample.Client.Extensions;
using AqueductExample.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AqueductExample.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();
            
            builder.AddAqueduct(configuration =>
            {
                configuration.SerialisableTypeList = new SerialisableTypesList();
                configuration.ServicesTypeList = new ServiceTypesList();
            });

            Console.WriteLine("Starting Aqueduct Example Client");

            await builder.Build().RunAsync();
        }
    }
}
