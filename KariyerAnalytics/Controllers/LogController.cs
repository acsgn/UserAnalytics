using System.Web;
using System.Web.Http;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;
using System.Collections.Generic;
using System;

namespace KariyerAnalytics.Controllers
{
    public class LogController : ApiController
    {

        private static List<Log> logs = new List<Log>();


        [HttpGet]
        public IEnumerable<Log> GetAll()
        {
            //    var logsdto = from l in logs
            //                select new LogDTO()
            //                {
            //                    Company = l.Company,
            //                    User = l.User,
            //                    URL = l.URL,
            //                    Date = l.Date,
            //                    IP = l.IP,
            //                    ResponseTime = l.ResponseTime
            //                };

            var engine = new LogEngine();
            engine.Search();

            return logs;
        }


        [HttpPost]
        public void Create(LogInformation info)
        {
            var engine = new LogEngine();
            engine.Add(info);
            //var log = new Log
            //{
            //    Company = info.CompanyName,
            //    User = info.Username,
            //    URL = info.URL,
            //    Endpoint = info.Endpoint,
            //    IP = HttpContext.Current.Request.UserHostAddress,
            //    Date = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
            //    ResponseTime = info.ResponseTime
            //};
            //logs.Add(log);

        }

    }
}
