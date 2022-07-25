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
    internal class GetWalletDetails
    {
        [FunctionName("GetWalletDetails")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "wallets/{walletId}")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "wallet",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery =
                "SELECT * FROM c " +
                "WHERE c.id = {walletId} ")]
        IEnumerable<Wallet> wallets,
        ILogger log)
        {
            return new OkObjectResult(wallets.FirstOrDefault());
        }
    }
}
