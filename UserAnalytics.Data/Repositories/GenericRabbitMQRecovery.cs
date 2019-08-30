using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserAnalytics.Data.Repositories;

namespace UserAnalytics.Data
{
    public class GenericRabbitMQRecovery<T> where T: class
    {
        private static volatile bool _IsActive;
        private static ConcurrentBag<KeyValuePair<string, T>> _Messages;
        private static GenericRabbitMQRepository<T> _RabbitMQRepository;
        static GenericRabbitMQRecovery()
        {
            _IsActive = false;
            _Messages = new ConcurrentBag<KeyValuePair<string, T>>();
            _RabbitMQRepository = new GenericRabbitMQRepository<T>();
        }
        public static void Insert(string key, T obj)
        {
            var request = new KeyValuePair<string, T>(key, obj);
            _Messages.Add(request);
            if (!_IsActive)
            {
                _IsActive = true;
                new Thread(new ThreadStart(Recover)).Start();
            }
        }

        private static void Recover()
        {
            while (!_Messages.IsEmpty)
            {
                KeyValuePair<string, T> message;
                if (_Messages.TryTake(out message))
                {
                    var result = _RabbitMQRepository.Queue(message.Key, message.Value);
                    if (!result)
                    {
                        _Messages.Add(message);
                        Task.Delay(1000);
                    }
                }
            }

            _IsActive = false;
        }

    }
}
