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
    internal class GetDaysTransferStats
    {
        [FunctionName("GetDaysTransferStats")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "wallets/{walletId}/transfers/stats/days/{days}")] HttpRequest req,
        [CosmosDB(
            databaseName: "wallet-niz",
            collectionName: "transfers",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT c.amount, udf.utcToDate(c.date) as date, c.walletId, c.category, c.id "+ 
            "FROM  c " +
            "WHERE c.walletId = {walletId} " +
            "AND udf.utcToDate(c.date) > udf.getDateBefore({days})" +
            "ORDER BY c.date DESC")]
        IEnumerable<TransferDto> transferDtos,
        ILogger log, [FromRoute] int days)
        {
            var income = 0.0;
            var transfersGroupedByDay = new List<IGrouping<DayOfWeek, TransferDto>>();

            if (transferDtos.Any())
            {
                income = transferDtos.Select(x => x.Amount).Sum();
                transfersGroupedByDay = transferDtos.GroupBy(x => x.Date.DayOfWeek).ToList();
            }

            log.LogInformation(income.ToString());
            log.LogInformation(transferDtos.Count().ToString());

            var stats = new PeriodTransferStats() { PeriodIncome = income };

            var today = DateTimeOffset.Now;
            
            for(var i = -days; i <= 0; i++)
            {
                var dayOfWeek = today.AddDays(i).DayOfWeek;

                var dayStat = new TransferDayStat { Day = dayOfWeek.ToString() };

                var group = transfersGroupedByDay.FirstOrDefault(x => x.Key == dayOfWeek);

                dayStat.Income = group == null ? 0 : group.Select(x => x.Amount).Sum();

                stats.DaysStats.Add(dayStat);
            }

            foreach(var day in stats.DaysStats)
            {
                stats.Days.Add(day.Day.Substring(0, 3));
                stats.Incomes.Add(day.Income);
            }

            return new OkObjectResult(stats);
        }
    }
}
