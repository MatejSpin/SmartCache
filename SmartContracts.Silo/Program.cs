using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using SmartCache.Grains;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SmartContracts.Silo
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = StartSilo().GetAwaiter().GetResult();
            Console.WriteLine("Silo started! Press any key to stop!");
            Console.ReadLine();
            host.StopAsync().GetAwaiter().GetResult();
            Console.WriteLine("Silo stopped! Press any key to close!");
            Console.ReadLine();
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .AddAzureBlobGrainStorage("BlobStore", options => options.ConnectionString = "UseDevelopmentStorage=true")
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "SmartCacheApp";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(EmailsGrain).Assembly).WithReferences());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
