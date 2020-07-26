using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    internal interface IEmailSenderService
    {
        Task SendEmailAsync(List<string> emails, string subject, string message);
    }
}