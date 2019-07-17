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
        public ResponseMetric GetBestResponseTime(Request request)
        {
            var engine = new LogEngine();
            return engine.GetBestResponseTime(request);
        }

        [HttpGet]
        public ResponseMetric GetWorstResponseTime(Request request)
        {
            var engine = new LogEngine();
            return engine.GetWorstResponseTime(request);
        }

        [HttpGet]
        public string[] GetCompanies(Request request)
        {
            var engine = new LogEngine();
            return engine.GetCompanies(request);
        }

        [HttpPost]
        public string[] GetDetailsofCompany(DetailRequest detailRequest)
        {
            var engine = new LogEngine();
            if (detailRequest.Username == null)
            {
                return engine.GetUsersofCompany(detailRequest.Company, (Request) detailRequest);
            }
            else
            {
                return engine.GetActionbyUserandCompany(detailRequest.Company, detailRequest.Username, (Request)detailRequest);
            }
        }

        [HttpGet]
        public string[] GetEndpoints(Request request)
        {
            var engine = new LogEngine();
            return engine.GetEndpoints(request);
        }

        [HttpPost]
        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var engine = new LogEngine();
            return engine.GetResponseTimes(responseTimeRequest.Endpoint, (Request) responseTimeRequest);
        }

    }
}
