using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WalletFunction.Models;

namespace WalletFunction
{
    public static class AddWalletFunction
    {
        [FunctionName("AddWalletFunction")]
            public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "wallet",
            ConnectionStringSetting = "CosmosDbConnectionString")]
        IAsyncCollector<dynamic> documentsOut,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<WalletDto>(requestBody);

            var wallet = new Wallet
            {
                Id = Guid.NewGuid(),
                Amount = data.Amount,
                Name = data.Name,
                UserId = data.UserId
            };
            await documentsOut.AddAsync(wallet);

            return new OkObjectResult(wallet.Id);     
        }
    }
}
