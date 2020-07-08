using System.Threading.Tasks;

using DAL.Contracts;

using Microsoft.Extensions.Configuration;

using RestSharp;

namespace DAL.Services
{
    public class InfoBipSendSmsService : ISmsSenderService
    {
        private readonly IConfiguration _configuration;

        public InfoBipSendSmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Send(string phoneNumber, string msg)
        {
            return await SendAsync(phoneNumber, msg, _configuration["InfoBipSettings:Sender"]);
        }

        public async Task<bool> SendAsync(string phoneNumber, string msg, string sender)
        {
            var client = new RestClient(_configuration["InfoBipSettings:ApiUrl"]);
            var request = new RestRequest { Resource = "plain" };
            request.AddParameter("user", _configuration["InfoBipSettings:User"]);
            request.AddParameter("password", _configuration["InfoBipSettings:Password"]);
            request.AddParameter("sender", sender);
            request.AddParameter("SMSText", msg);
            request.AddParameter("GSM", _configuration["InfoBipSettings:CountryCode"] + phoneNumber);
            request.Method = Method.GET;

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }

        bool ISmsSenderService.SendAsync(string phoneNumber, string msg, string sender)
        {
            throw new System.NotImplementedException();
        }
    }
}