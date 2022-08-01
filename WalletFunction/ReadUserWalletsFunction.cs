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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{userId}/wallets")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "wallet",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = 
                "SELECT * FROM c " +
                "WHERE c.UserId = {userId} " +
                "ORDER BY c.Name DESC" )]
        IEnumerable<Wallet> wallets)
        {
            return new OkObjectResult(wallets);
        }
    }
}
