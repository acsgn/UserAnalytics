using System.ServiceProcess;
using ServiceProcess.Helpers;

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
            //ServiceBase.Run(ServicesToRun);
            ServicesToRun.LoadServices();
        }
    }
}
