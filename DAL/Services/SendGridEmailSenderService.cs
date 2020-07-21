using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace DAL.Services
{
    public class SendGridEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailSenderService(IOptions<SendGridEmailAuthOptions> optionsAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            Options = optionsAccessor.Value;
        }

        public SendGridEmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Options = optionsAccessor.Value;
        }

        public SendGridEmailAuthOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(List<string> emails, string subject, string message)
        {
            //            return Execute(Environment.GetEnvironmentVariable("SENDEMAILDEMO_ENVIRONMENT_SENDGRID_KEY"), subject, message, emails);
            return Execute(_configuration["SendGrid:ApiKey"], subject, message, emails);
        }

        public static Task Execute(string apiKey, string subject, string message, List<string> emails)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                //From = new EmailAddress("noreply@" + _configuration["SendGrid:Domain"], _configuration["SendGrid:Name"]),
                From = new EmailAddress("lenepalu@gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            if (emails != null)
            {
                foreach (var email in emails)
                {
                    msg.AddTo(new EmailAddress(email));
                }
            }
           

            Task response = client.SendEmailAsync(msg);
            return response;
        }
    }
}