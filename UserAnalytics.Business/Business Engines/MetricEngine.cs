﻿using UserAnalytics.Business.Contract;
using UserAnalytics.Service.Entities;
using UserAnalytics.Data.Contract;
using System.Linq;

namespace UserAnalytics.Business
{
    public class MetricEngine : IMetricEngine
    {
        private readonly IMetricRepository _MetricRepository;

        public MetricEngine(IMetricRepository metricRepository)
        {
            _MetricRepository = metricRepository;
        }

        public MetricsResponseDTO[] GetCompanyMetrics(MetricRequest request)
        {
            var result = _MetricRepository.GetCompanyMetrics(request.Endpoint, request.Size, request.Ascending, request.After, request.Before);
            return (from r in result
                    select new MetricsResponseDTO
                    {
                        Key = r.Key,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }

        public MetricsResponseDTO[] GetEndpointMetrics(MetricRequest request)
        {
            var result = _MetricRepository.GetEndpointMetrics(request.CompanyName, request.Username, request.Size, request.Ascending, request.After, request.Before);
            return (from r in result
                    select new MetricsResponseDTO
                    {
                        Key = r.Key,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }

        public MetricsResponseDTO GetSingleMetric(MetricRequest request)
        {
            var result = _MetricRepository.GetSingleMetric(request.CompanyName, request.Username, request.Endpoint, request.After, request.Before);
            return new MetricsResponseDTO
            {
                Key = result.Key,
                NumberOfRequests = result.NumberOfRequests,
                MinResponseTime = result.MinResponseTime,
                AverageResponseTime = result.AverageResponseTime,
                MaxResponseTime = result.MaxResponseTime
            };
        }
    }
}
