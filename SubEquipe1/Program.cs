using StackExchange.Redis;
using SubEquipe1.Domain.Interface;
using SubEquipe1.Infra.Ioc;
using System;
using System.Threading.Tasks;

namespace SubEquipe1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            Console.Write(Environment.NewLine);

            string completeAwnser = string.Empty;

            try
            {
                var messageRepository = DependencyInjector.GetService<IMessageRepository>();

                var awnserRepository = DependencyInjector.GetService<IAwnserRepository>();

                messageRepository.Subscribe((ch, msg) =>
                {
                    string awnser = string.Empty;

                    Console.WriteLine($"Message received: {msg}");

                    if (!ValidateMessage(msg))
                    {
                        Console.WriteLine("Invalid message!");
                        Console.Write(Environment.NewLine);
                    }
                    else
                    {
                        var questionId = msg.ToString().Split(":")[0];
                        var question = msg.ToString().Split(":")[1];

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
                        messageRepository.Send(completeAwnser);
                    }
                });

                Console.Write(Environment.NewLine);
                Console.WriteLine("Waiting for message");
                Console.Write(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadKey();
        }

        private static bool ValidateMessage(RedisValue msg)
        {
            bool result = false;

            var msgArray = msg.ToString().Split(':');

            if (!msg.IsNullOrEmpty && msg.ToString().ToLower().StartsWith("p") && msgArray.Length == 2)
                result = true;

            return result;
        }
    }
}