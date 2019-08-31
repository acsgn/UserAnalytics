using System;
using System.ServiceProcess;
using System.Timers;
using UserAnalytics.Business.Contract;
using UserAnalytics.Common.DependencyInjection;

namespace UserAnalytics.Service.QueueConsumer
{
    public partial class QueueConsumer : ServiceBase
    {
        private ILogRabbitMQEngine _RabbitMQEngine;
        private ILogElasticsearchEngine _ElasticsearchEngine;

        private Timer _Timer;
        public QueueConsumer()
        {
            InitializeLifetimeService();
            _Timer = new Timer(1000);
            _RabbitMQEngine = DILoader.ResolveLogRabbitMQEngine();
            _ElasticsearchEngine = DILoader.ResolveLogElasticsearchEngine();

            _Timer.Elapsed += _Timer_Elapsed;
        }

        protected override void OnStart(string[] args)
        {
            _RabbitMQEngine.GetMany(_ElasticsearchEngine.AddMany);

            _Timer.Start();
        }

        protected override void OnStop()
        {
            _RabbitMQEngine.StopConsumer();

            _Timer.Stop();
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _Timer.Stop();
            if (!_RabbitMQEngine.IsWorking())
            {
                try
                {
                    _RabbitMQEngine.GetMany(_ElasticsearchEngine.AddMany);
                }
                catch (Exception)
                {
                }
            }
            _Timer.Start();
        }
    }
}
