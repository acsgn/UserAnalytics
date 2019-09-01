using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserAnalytics.Data
{
    public class GenericBulkRabbitMQConsumer<T> : EventingBasicConsumer where T : class
    {
        private static readonly int _DelayBetweenTries = 2000;

        private List<T> _Documents;
        private readonly int _Bulk;
        private readonly Func<IEnumerable<T>, bool> _Func;

        public GenericBulkRabbitMQConsumer(int bulk, Func<IEnumerable<T>, bool> func, IModel model) : base(model)
        {
            _Documents = new List<T>();
            _Bulk = bulk;
            _Func = func;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var json = Encoding.UTF8.GetString(body);
            var document = JsonConvert.DeserializeObject<T>(json);

            _Documents.Add(document);

            if (_Documents.Count >= _Bulk)
            {
                var ack = _Func(_Documents);
                while (!ack)
                {
                    Task.Delay(_DelayBetweenTries).Wait();
                    ack = _Func(_Documents);
                }
                Model.BasicAck(deliveryTag, true);
                _Documents.Clear();
            }

        }
    }
}
