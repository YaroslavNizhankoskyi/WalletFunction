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
using System.Collections.Generic;

namespace WalletFunction
{
    internal static class ReadUserWalletsFunction
    {
        [FunctionName("ReadUserWalletsFunction")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "wallet",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = 
                "SELECT * FROM c " +
                "ORDER BY name DESC" +
                "WHERE userId={id}")]
        IEnumerable<Wallet> wallets,
        ILogger log)
        {
            return new OkObjectResult(wallets);
        }
    }
}
