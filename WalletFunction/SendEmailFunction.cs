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

namespace WalletFunction
{
    internal static class SendEmailFunction
    {
        [FunctionName("EmailBusTrigger")]
        public static async Task Run(
        [ServiceBusTrigger("email", Connection = "ServiceBusConnectionString")]
        EmailMessageDto emailDto,
        DateTime enqueuedTimeUtc,
        string messageId,
        ILogger log)
        {
            log.LogInformation($"User Email: {emailDto.UserEmail}");
            log.LogInformation($"Time: {enqueuedTimeUtc}");
            log.LogInformation($"MessageId: {messageId}");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("SenderEmail")));
            email.To.Add(MailboxAddress.Parse(emailDto.UserEmail));
            email.Subject = "Low budget";
            email.Body = new TextPart(TextFormat.Plain) 
            {
                Text = $"Your wallet {emailDto.WalletName}, has low blance of {emailDto.LeftAmount}" 
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate(Environment.GetEnvironmentVariable("SenderEmail"),
                Environment.GetEnvironmentVariable("SenderPassword"));
            var response = await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }
    }
}
