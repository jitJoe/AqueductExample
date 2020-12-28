using Aqueduct.Server.Extensions;
using Aqueduct.Server.Transport.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AqueductExample.Server.Services;
using AqueductExample.Shared;

namespace AqueductExample.Server
{
    public class Startup
    {
        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR(options =>
            {
                options.StreamBufferCapacity = 30;
                options.MaximumParallelInvocationsPerClient = 10;
            });
            
            services.AddOptions();

            services.AddAqueduct(configuration =>
            {
                configuration.CallbackTimeoutMillis = 30_000;
                configuration.SerialisableTypeList = new SerialisableTypesList();
                configuration.ServicesTypeList = new ServiceTypesList();
            });

            services.AddHostedService<RandomNumberHostedService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();

                endpoints.MapHub<SignalRHubInboundTransportDriver>("/aqueduct");

                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
