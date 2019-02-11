using System.Threading.Tasks;

namespace SubEquipe1.Domain.Interface
{
    public interface IAwnserRepository
    {
        Task<string> AskTheQuestion(string question);
    }
}