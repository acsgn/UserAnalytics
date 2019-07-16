using System.Web;
using System.Web.Http;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Controllers
{
    public class LogController : ApiController
    {
        [HttpPost]
        public void Create(LogInformation info)
        {
            info.IP = HttpContext.Current.Request.UserHostAddress;
            info.Timestamp = HttpContext.Current.Timestamp;
            var engine = new LogEngine();
            engine.Add(info);
        }

        [HttpGet]
        public ResponseMetric GetBestResponseTime()
        {
            var engine = new LogEngine();
            return engine.GetBestResponseTime();
        }

        [HttpGet]
        public ResponseMetric GetWorstResponseTime()
        {
            var engine = new LogEngine();
            return engine.GetWorstResponseTime();
        }

        [HttpGet]
        public string[] GetCompanies()
        {
            var engine = new LogEngine();
            return engine.GetCompanies();
        }

        [HttpPost]
        public string[] GetDetailsofCompany(DetailRequest detailRequest)
        {
            var engine = new LogEngine();
            if (detailRequest.Username == null)
            {
                return engine.GetUsersofCompany(detailRequest.Company);
            }
            else
            {
                return engine.GetActionbyUserandCompany(detailRequest.Company, detailRequest.Username);
            }
        }

        [HttpGet]
        public string[] GetEndpoints()
        {
            var engine = new LogEngine();
            return engine.GetEndpoints();
        }

        [HttpPost]
        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var engine = new LogEngine();
            return engine.GetResponseTimes(responseTimeRequest.Endpoint);
        }

    }
}
