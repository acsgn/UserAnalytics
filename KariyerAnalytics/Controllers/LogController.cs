using System.Web;
using System.Web.Http;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Business.Contract;

namespace KariyerAnalytics.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogElasticsearchEngine _LogEngine;

        public LogController(ILogElasticsearchEngine logEngine)
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
