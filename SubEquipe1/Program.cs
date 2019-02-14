using SubEquipe1.Domain.Interface;
using SubEquipe1.Infra.Ioc;
using System;

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
                var repository = DependencyInjector.GetService<IMessageRepository>();

                repository.Subscribe((ch, msg) =>
                {
                    string awnser = string.Empty;

                    Console.WriteLine($"Message received: {msg}");

                    var questionId = msg.ToString().Split(":")[0];

                    completeAwnser = $"{questionId}:Equipe01:Awnser";

                    repository.Send(completeAwnser);
                });

                Console.Write(Environment.NewLine);
                Console.WriteLine("Waiting for message");

                Console.Write(Environment.NewLine);
                Console.WriteLine("Click to finish");
                Console.Write(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadKey();
        }
    }
}
