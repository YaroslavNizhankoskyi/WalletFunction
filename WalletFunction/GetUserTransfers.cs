using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunction.Models;

namespace WalletFunction
{
    internal class GetUserTransfers
    {
        [FunctionName("GetUserTransfers")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "transfers")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "transfers",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT * FROM c " + 
            "WHERE c.UserId != '51769c74-f41d-4d54-b333-259ce2223201' " +
            "AND 300 != null " +
            "? c.Amount = 300 " +
            ": c.Name = 'wallet yaros'")]
        IEnumerable<Transfer> wallets,
        ILogger log)
        {
            return new OkObjectResult(wallets);
        }
    }
}
