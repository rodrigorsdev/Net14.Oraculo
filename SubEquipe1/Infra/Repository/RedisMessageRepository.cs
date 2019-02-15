using StackExchange.Redis;
using SubEquipe1.Domain.Entiity;
using SubEquipe1.Domain.Interface;
using System;

namespace SubEquipe1.Infra.Repository
{
    public class RedisMessageRepository : IRedisMessageRepository
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;
        private readonly string _channel;

        public RedisMessageRepository(string connectionString, string channel)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _db = _connection.GetDatabase();
            _channel = channel;
        }

        public void Subscribe(Action<RedisChannel, RedisValue> handler)
        {
            var sub = _connection.GetSubscriber();
            sub.Subscribe(_channel, handler);
        }

        public void Send(Message entity)
        {
            _db.HashSet(entity.Question, entity.Team, entity.Answer);
        }
    }
}
