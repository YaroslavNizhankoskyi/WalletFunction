using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction
{
    internal static class SendEmailFunction
    {
        [FunctionName("EmailBusTrigger")]
        public static void Run(
        [ServiceBusTrigger("email", Connection = "ServiceBusConnectionString")]
        string email,
        DateTime enqueuedTimeUtc,
        string messageId,
        ILogger log)
        {
            log.LogInformation($"Email: {email}");
            log.LogInformation($"Time: {enqueuedTimeUtc}");
            log.LogInformation($"MessageId: {messageId}");
        }
    }
}
