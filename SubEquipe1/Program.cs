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

                        if (DependencyInjector.UseDefaultAwnser)
                            awnser = "Desculpe, não sei a resposta para a sua pergunta!";
                        else
                            awnser = awnserRepository.AskTheQuestion(question).Result;

                        completeAwnser = $"{questionId}:Equipe01:{awnser}";

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