using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubEquipe1.Domain.Interface;
using SubEquipe1.Infra.Repository;
using System;
using System.IO;

namespace SubEquipe1.Infra.Ioc
{
    public static class DependencyInjector
    {
        private static IServiceProvider ServiceProvider { get; set; }

        private static IServiceCollection Services { get; set; }

        public static string TeamName { get; private set; }

        public static T GetService<T>()
        {
            Services = Services ?? RegisterServices();
            ServiceProvider = ServiceProvider ?? Services.BuildServiceProvider();
            return ServiceProvider.GetService<T>();
        }

        public static IServiceCollection RegisterServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            TeamName = configuration["Team:Name"];

            return RegisterServices(new ServiceCollection(), configuration);
        }

        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;

            services.AddSingleton<IMessageRepository>(a => new MessageRepository(configuration["Redis:Url"], configuration["Redis:Channel"]));
            services.AddSingleton<IAwnserRepository, AwnserRepository>();

            return Services;
        }
    }
}