using System.Web;
using System.Web.Http;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Business.Contract;

namespace KariyerAnalytics.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogEngine _LogEngine;

        public LogController(ILogEngine logEngine)
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
