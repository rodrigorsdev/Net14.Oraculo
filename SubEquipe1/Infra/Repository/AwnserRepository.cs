using HtmlAgilityPack;
using SubEquipe1.Domain.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace SubEquipe1.Infra.Repository
{
    public class AwnserRepository : IAwnserRepository
    {
        public async Task<string> AskTheQuestion(string question)
        {
            HtmlWeb htmlWeb;
            HtmlDocument htmlDocument;
            HtmlNode htmlNode;

            htmlWeb = new HtmlWeb();
            htmlDocument = await htmlWeb.LoadFromWebAsync($"https://www.google.com/search?source=hp&ei=8MthXKSdHqm-5OUP2KyM0A0&q={question}&btnK=Pesquisa+Google&oq={question}&gs_l=psy-ab.3..0j38j0i22i30.1069.4965..5256...2.0..0.141.1640.7j10......0....1..gws-wiz.....0..0i131j0i10.BSDYJDuFLQM");
            question = question.Replace(' ', '+');

            //Primeira tentativa 
            htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class=\"mraOPb\"]");

            if (htmlNode != null)
            {
                return htmlNode.FirstChild.FirstChild.InnerText;
            }
            //Primeira tentativa 

            //Segunda tentativa
            htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class=\"FSP1Dd\"]");

            if (htmlNode != null)
            {
                return htmlNode.InnerText;
            }
            //Segunda tentativa

            //Terceira tentativa
            htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class=\"KpMaL\"]");

            if (htmlNode != null)
            {
                return htmlNode.InnerText;         
            }
            //Terceira tentativa

            //Quarta tentativa
            htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//span[@class=\"mrH1y\"]");

            if (htmlNode != null)
            {
                return htmlNode.InnerText;                 
            }
            //Quarta tentativa

            return string.Empty;
        }
    }
}