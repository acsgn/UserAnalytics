using System.ServiceProcess;
using ServiceProcess.Helpers;

namespace UserAnalytics.Service.QueueConsumer
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
