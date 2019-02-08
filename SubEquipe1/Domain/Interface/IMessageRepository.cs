using SubEquipe1.Domain.Entiity;

namespace SubEquipe1.Domain.Interface
{
    public interface IMessageRepository
    {
        void Send(Message entity);
    }
}
