using System.Web;
using System.Web.Http;
using UserAnalytics.Service.Entities;
using UserAnalytics.Business.Contract;

namespace UserAnalytics.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogRabbitMQEngine _LogEngine;

        public LogController(ILogRabbitMQEngine logEngine)
        {
            _LogEngine = logEngine;
        }

        [HttpPost]
        public void Create(LogRequest logRequest)
        {
            logRequest.IP = HttpContext.Current.Request.UserHostAddress;
            logRequest.Timestamp = HttpContext.Current.Timestamp;
            _LogEngine.Add(logRequest);
        }
    }
}
