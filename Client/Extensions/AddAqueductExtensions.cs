using System;
using Aqueduct.Client;
using Aqueduct.Client.ServiceProvider;
using Aqueduct.Client.Transport;
using Aqueduct.Client.Transport.SignalR;
using Aqueduct.Shared;
using Aqueduct.Shared.CallbackRegistry;
using Aqueduct.Shared.DateTime;
using Aqueduct.Shared.Proxy;
using Aqueduct.Shared.Serialisation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AqueductExample.Client.Extensions
{
    public static class AddAqueductExtensions
    {
        public static void AddAqueduct(this WebAssemblyHostBuilder builder, Action<AqueductClientConfiguration> configure)
        {
            var clientConfiguration = new AqueductClientConfiguration();

            configure(clientConfiguration);
            
            builder.Services.AddSingleton<AqueductSharedConfiguration>(new AqueductSharedConfiguration
            {
                CallbackTimeoutMillis = clientConfiguration.CallbackTimeoutMillis
            });

            var typeFinder = new TypeFinder();
            typeFinder.RegisterTypeList("Serialisable", clientConfiguration.SerialisableTypeList);
            typeFinder.RegisterTypeList("Services", clientConfiguration.ServicesTypeList);

            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<ITypeFinder>(typeFinder);
            builder.Services.AddSingleton<IProxyProvider, ProxyProvider>();
            builder.Services.AddSingleton<IClientServiceProvider, ClientServiceProvider>();
            builder.Services.AddSingleton<ISerialisationDriver, JsonNetSerialisationDriver>();
            builder.Services.AddSingleton<ICallbackRegistry, CallbackRegistry>();
            builder.Services.AddSingleton<IClientTransportDriver, SignalRClientTransportDriver>();
        }
    }
}