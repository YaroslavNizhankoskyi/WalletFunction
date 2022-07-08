using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunction;

[assembly: WebJobsStartup(typeof(Startup))]
namespace WalletFunction
{
    internal class Startup : IWebJobsStartup
    {
        public Startup(ILogger log)
        {
            Log = log;
        }

        public ILogger Log { get; }

        public void Configure(IWebJobsBuilder builder)
        {
            Log.LogInformation("Started startup");
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();
            builder.Services.PostConfigure<CosmosDBOptions>(options =>
            {
                options.ConnectionString = config["CosmosDbConnectionString"];
            });
        }
    }
}
