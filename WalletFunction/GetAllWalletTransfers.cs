﻿using Microsoft.AspNetCore.Http;
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
    internal class GetAllUserTransfer
    {
        [FunctionName("GetAllWalletTransfers")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "wallets/{walletId}/transfers")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "transfers",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT TOP 10 * FROM c " +
            "WHERE c.walletId = {walletId} " +
            "ORDER BY c.date DESC")]
        IEnumerable<TransferDto> transferDtos)
        {
            return new OkObjectResult(transferDtos);
        }
    }
}
