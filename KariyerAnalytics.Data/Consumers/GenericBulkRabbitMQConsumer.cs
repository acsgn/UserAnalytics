using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace KariyerAnalytics.Data
{
    public class GenericBulkRabbitMQConsumer<T> : EventingBasicConsumer where T : class
    {
        private List<T> _Documents = new List<T>();
        private readonly int _Bulk;

        public event BulkIndex AddMany;
        public delegate bool BulkIndex(IEnumerable<T> documents);

        public GenericBulkRabbitMQConsumer(int bulk, IModel model) : base(model)
        {
            _Bulk = bulk;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var json = Encoding.UTF8.GetString(body);
            var document = JsonConvert.DeserializeObject<T>(json);

            _Documents.Add(document);

            if (_Documents.Count >= _Bulk)
            {
                var ack = AddMany(_Documents);
                while (!ack)
                {
                    ack = AddMany(_Documents);
                }
                Model.BasicAck(deliveryTag, true);
                _Documents.Clear();
            }

        }
    }
}
