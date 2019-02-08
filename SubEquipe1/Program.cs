using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.IO;

namespace SubEquipe1
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static ConnectionMultiplexer _connection;
        static IDatabase _db;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            
            _connection = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis:Url"));
            _db = _connection.GetDatabase();

            var sub = _connection.GetSubscriber();
            sub.Subscribe(_configuration.GetConnectionString("Redis:Channel"), (ch, msg) =>
            {
                var separetedQuestion = msg.ToString().Split(":");



                _db.HashSet(separetedQuestion[0], "Equipe1", "Resposta");
            });

            Console.ReadKey();
        }
    }
}
