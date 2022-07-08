using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public void Configure(IWebJobsBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();
            builder.Services.PostConfigure<CosmosDBOptions>(options =>
            {
                options.ConnectionString = Environment.GetEnvironmentVariable("CosmosDbConnectionString");
            });
        }
    }
}
