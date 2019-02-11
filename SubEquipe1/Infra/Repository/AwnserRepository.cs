using SubEquipe1.Domain.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace SubEquipe1.Infra.Repository
{
    public class AwnserRepository : IAwnserRepository
    {
        public async Task<string> AskTheQuestion(string question)
        {
            using(HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"http://google.com/q?={question}");
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
        }
    }
}
