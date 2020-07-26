using System.Threading.Tasks;

using DAL.Model;

using Microsoft.Extensions.Configuration;

using RestSharp;

using Services;
using Services.Contracts;

namespace DAL.Services
{
    public class InfoBipSendSmsService : ISmsSenderService
    {
        private readonly AppSettings _configuration;

        public InfoBipSendSmsService(AppSettings configuration)
        {
            _configuration = configuration;
        }

        public bool Send(string phoneNumber, string msg)
        {
            return Send(phoneNumber, msg, _configuration.Sender);
        }

        public bool Send(string phoneNumber, string msg, string sender)
        {
            var client = new RestClient(_configuration.ApiUrl);
            var request = new RestRequest { Resource = "plain" };
            request.AddParameter("user", _configuration.User);
            request.AddParameter("password", _configuration.Password);
            request.AddParameter("sender", sender);
            request.AddParameter("SMSText", msg);
            request.AddParameter("GSM", _configuration.CountryCode + phoneNumber);
            request.Method = Method.GET;

            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }
    }
}