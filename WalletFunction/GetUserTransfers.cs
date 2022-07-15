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
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "transfers/{walletId}/{category}")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "transfers",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT * FROM c " +
            "WHERE c.walletId = {walletId} " +
            "AND c.category = {category}")]
        IEnumerable<TransferDto> transferDtos,
        ILogger log)
        {
            return new OkObjectResult
                (
                    transferDtos.Select(x => new Transfer
                    {
                        Amount = x.Amount,
                        Category = x.Category,
                        WalletId = Guid.Parse(x.WalletId),
                        Id = Guid.Parse(x.Id)
                    })
                );
        }
    }
}
