using SubEquipe1.Domain.Interface;
using SubEquipe1.Infra.Ioc;
using System;

namespace SubEquipe1
{
    public class ConsoleFunction
    {
        public void ShowMessages(string message, string awnser, string complete, string completeAwnser)
        {
            var awnserRepository = DependencyInjector.GetService<IAwnserRepository>();

            Console.WriteLine($"Message received: {message}");

            if (!ValidateMessage(message))
            {
                Console.WriteLine("Invalid message!");
                Console.Write(Environment.NewLine);
            }
            else
            {
                var questionId = message.ToString().Split(":")[0];
                var question = message.ToString().Split(":")[1];

                if (question.Contains("+"))
                {
                    var separatedNumbers = question.Split("+");
                    var firstNumber = Convert.ToInt32(separatedNumbers[0].Substring(10));
                    var secondNumber = Convert.ToInt32(separatedNumbers[1].Replace("?", string.Empty));
                    awnser = $"{firstNumber + secondNumber}";
                }
                else
                {
                    awnser = awnserRepository.AskTheQuestion(question).Result;

                    if (string.IsNullOrEmpty(awnser))
                        awnser = "Desculpe, não sei a resposta para a sua pergunta!";
                }

                completeAwnser = $"{questionId}:{DependencyInjector.TeamName}:{awnser}";

                Console.WriteLine("Resposta: " + awnser);
            }
        }

        private static bool ValidateMessage(string message)
        {
            bool result = false;

            var msgArray = message.ToString().Split(':');

            if (!string.IsNullOrEmpty(message) && message.ToString().ToLower().StartsWith("p") && msgArray.Length == 2)
                result = true;

            return result;
        }
    }
}
