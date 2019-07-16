using System.Web;
using System.Web.Http;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Controllers
{
    public class LogController : ApiController
    {

        [HttpGet]
        public void GetBestAndWorstResponseTime()
        {
            var engine = new LogEngine();
            engine.GetBestAndWorstResponseTime();
            engine.GetCompanies();
            engine.GetUsersofCompany("Kariyer");
            engine.GetActionbyUserandCompany("Kariyer", "Ali");
        }


        [HttpPost]
        public void Create(LogInformation info)
        {
            info.IP = HttpContext.Current.Request.UserHostAddress;
            info.Timestamp = HttpContext.Current.Timestamp;
            var engine = new LogEngine();
            engine.Add(info);
        }

    }
}
