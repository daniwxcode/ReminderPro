using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ISmsSenderService
    {
        Task<bool> Send(string phoneNumber, string msg);

        bool SendAsync(string phoneNumber, string msg, string sender);
    }
}