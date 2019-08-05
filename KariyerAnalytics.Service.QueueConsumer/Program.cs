using System.ServiceProcess;

namespace KariyerAnalytics.Service.QueueConsumer
{
    static class Program
    {
        static void Main()
        {
            var ServicesToRun = new ServiceBase[]
            {
                new QueueConsumer()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
