using StackExchange.Redis;
using SubEquipe1.Domain.Entiity;
using System;

namespace SubEquipe1.Domain.Interface
{
    public interface IRedisMessageRepository
    {
        void Subscribe(Action<RedisChannel, RedisValue> handler);
        void Send(Message entity);
    }
}
