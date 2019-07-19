using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KariyerAnalytics.Business;
using KariyerAnalytics.Client.Entities;

namespace KariyerAnalytics.Controllers
{
    public class StatisticController : ApiController
    {
        [HttpGet]
        public MetricResponse GetBestResponseTime(Request request)
        {
            var engine = new LogEngine();
            return engine.GetBestResponseTime(request);
        }

        [HttpGet]
        public MetricResponse GetWorstResponseTime(Request request)
        {
            var engine = new LogEngine();
            return engine.GetWorstResponseTime(request);
        }

        [HttpGet]
        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var engine = new LogEngine();
            return engine.GetResponseTimes(responseTimeRequest.Endpoint, responseTimeRequest);
        }


        [HttpGet]
        public string[] GetEndpoints(Request request)
        {
            var engine = new LogEngine();
            return engine.GetEndpoints(request);
        }
    }
}
