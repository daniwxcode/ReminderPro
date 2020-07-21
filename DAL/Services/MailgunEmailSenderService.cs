using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DAL.Contracts;
using RestSharp;
using RestSharp.Authenticators;

namespace DAL.Services
{
    public class MailgunEmailSenderService:IEmailSenderService
    {
//        public static IRestResponse SendSimpleMessage()
//        {
//            RestClient client = new RestClient
//            {
//                BaseUrl = new Uri("https://api.mailgun.net/v3"),
//                Authenticator = new HttpBasicAuthenticator("api", "f38a91734e6597ec0be9c50c014d66a4-ffefc4e4-c1e978d7")
//            };
//            RestRequest request = new RestRequest();
//            request.AddParameter("domain", "sandbox652055836f014300a4f55e32601ed020.mailgun.org", ParameterType.UrlSegment);
//            request.Resource = "{domain}/messages";
//            request.AddParameter("from", "Ma Banque <no-reply@sandbox652055836f014300a4f55e32601ed020.mailgun.org>");
//            request.AddParameter("to", "lenepalu@gmail.com");
//            request.AddParameter("subject", "Hello");
//            request.AddParameter("text", "Testing some Mailgun awesomness!");
//            request.Method = Method.POST;
//            return client.Execute(request);
//        }

        public static Task SendEmailAsync(List<string> emails, string subject, string message)
        {
            RestClient client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", "f38a91734e6597ec0be9c50c014d66a4-ffefc4e4-c1e978d7")
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox652055836f014300a4f55e32601ed020.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Ma Banque <no-reply@sandbox652055836f014300a4f55e32601ed020.mailgun.org>");
            if (emails!=null)
            {
                foreach (var email in emails)
                {
                     request.AddParameter("to", email);
                }
            }
           
            request.AddParameter("subject", subject);
            request.AddParameter("text", message);
            request.AddParameter("html", message);
            request.Method = Method.POST;
            return client.ExecuteAsync(request);
        }

        Task IEmailSenderService.SendEmailAsync(List<string> emails, string subject, string message)
        {
            return SendEmailAsync(emails, subject, message);
        }
    }
}
