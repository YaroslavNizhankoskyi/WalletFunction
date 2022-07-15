using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunction.Models;
using SendGrid.Helpers.Mail;

namespace WalletFunction
{
    internal static class SendEmailFunction
    {
        [FunctionName("EmailBusTrigger")]
        [return: SendGrid(ApiKey = "SendGridApiKey")]
        public static SendGridMessage Run(
        [ServiceBusTrigger("email", Connection = "ServiceBusConnectionString")]
        EmailMessageDto emailDto,
        DateTime enqueuedTimeUtc,
        string messageId,
        ILogger log)
        {
            log.LogInformation($"User Email: {emailDto.UserEmail}");
            log.LogInformation($"Time: {enqueuedTimeUtc}");
            log.LogInformation($"MessageId: {messageId}");

            log.LogInformation($"SendEmailTimer executed at: {DateTime.Now}");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Environment.GetEnvironmentVariable("SenderEmail"), "Yaroslav Nizhankovskyi"),
                Subject = $"You have low budget on {emailDto.WalletName}",
                PlainTextContent = $"Your wallet {emailDto.WalletName} has low budget of {emailDto.LeftAmount}"
            };
            
            msg.AddTo(new EmailAddress(emailDto.UserEmail, emailDto.UserEmail));

            return msg;
        }
    }
}
