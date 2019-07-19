using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Business
{
    public class StatisticEngine : IStatisticEngine
    {
        public MetricResponse GetBestResponseTime(Request request)
        {
            var statisticRepository = new StatisticRepository();
            var response = statisticRepository.GetBestResponseTime(request.After, request.Before);
            return new MetricResponse
            {
                Endpoint = response.Endpoint,
                ResponseTime = response.ResponseTime
            };
        }

        public MetricResponse GetWorstResponseTime(Request request)
        {
            var statisticRepository = new StatisticRepository();
            var response = statisticRepository.GetWorstResponseTime(request.After, request.Before);
            return new MetricResponse
            {
                Endpoint = response.Endpoint,
                ResponseTime = response.ResponseTime
            };
        }

        public string[] GetEndpoints(Request request)
        {
            var statisticRepository = new StatisticRepository();
            return statisticRepository.GetEndpoints(request.After, request.Before);
        }

        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var statisticRepository = new StatisticRepository();
            return statisticRepository.GetResponseTimes(responseTimeRequest.Endpoint, responseTimeRequest.After, responseTimeRequest.Before);
        }
        
    }
}
