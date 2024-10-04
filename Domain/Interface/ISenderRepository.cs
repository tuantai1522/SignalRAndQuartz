using Domain.Entity;

namespace Domain.Interface
{
    public interface ISenderRepository
    {
        Task<Sender> GetSenderByUserName(string userName);
        Task<IList<Sender>> GetAllSenders();
        Task CreateSender(Sender sender);
    }
}
