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
    internal class GetWeekTransferStats
    {
        [FunctionName("GetWeekTransferStats")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "transfers/{walletId}")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "transfers",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT c.amount, udf.utcToDate(c.date) as date, c.walletId, c.category, c.id "+ 
            "FROM  c " +
            "WHERE c.walletId = {walletId} " +
            "AND udf.utcToDate(c.date) > udf.lastWeek()" +
            "ORDER BY c.date DESC")]
        IEnumerable<TransferDto> transferDtos,
        ILogger log)
        {



            return new OkObjectResult(transferDtos);
        }
    }
}