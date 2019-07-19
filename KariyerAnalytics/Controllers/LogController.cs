using System.Web;
using System.Web.Http;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Business;
using System.Collections.Generic;

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
    }
}
