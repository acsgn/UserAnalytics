﻿using System.ComponentModel.Composition;
using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Common.DependencyInjection;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StatisticController : ApiController
    {
        [Import]
        private IStatisticEngine _StatisticEngine;

        public StatisticController()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }

        [HttpGet]
        public MetricResponse GetBestResponseTime(Request request)
        {
            return _StatisticEngine.GetBestResponseTime(request);
        }

        [HttpGet]
        public MetricResponse GetWorstResponseTime(Request request)
        {
            return _StatisticEngine.GetWorstResponseTime(request);
        }
        
        [HttpGet]
        public string[] GetEndpoints(Request request)
        {
            return _StatisticEngine.GetEndpoints(request);
        }

        [HttpGet]
        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            return _StatisticEngine.GetResponseTimes(responseTimeRequest);
        }

    }
}
