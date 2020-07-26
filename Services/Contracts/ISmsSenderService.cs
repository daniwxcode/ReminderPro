namespace Services.Contracts
{
    public interface ISmsSenderService
    {
        bool Send(string phoneNumber, string msg);

        bool Send(string phoneNumber, string msg, string sender);
    }
}