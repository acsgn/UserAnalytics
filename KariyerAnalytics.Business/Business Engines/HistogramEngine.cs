﻿using System.Linq;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business
{
    public class HistogramEngine : IHistogramEngine
    {
        private readonly IHistogramRepository _HistogramRepository;

        public HistogramEngine(IHistogramRepository histogramRepository)
        {
            _HistogramRepository = histogramRepository;
        }

        public HistogramResponseDTO[] GetResponseTimesHistogram(ResponseTimesHistogramRequest histogramRequest)
        {
            var result = _HistogramRepository.GetResponseTimesHistogram(histogramRequest.Interval, histogramRequest.After, histogramRequest.Before);
            return (from r in result
                    select new HistogramResponseDTO
                    {
                        Average = r.Average,
                        NumberOfRequests = r.NumberOfRequests,
                        Timestamp = r.Timestamp
                    }).ToArray();
        }

        public HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(EndpointResponseTimesHistogramRequest endpointHistogramRequest)
        {
            var result = _HistogramRepository.GetResponseTimesHistogramByEndpoint(endpointHistogramRequest.Endpoint, endpointHistogramRequest.Interval, endpointHistogramRequest.After, endpointHistogramRequest.Before);
            return (from r in result
                    select new HistogramResponseDTO
                    {
                        Average = r.Average,
                        NumberOfRequests = r.NumberOfRequests,
                        Timestamp = r.Timestamp
                    }).ToArray();
        }
    }
}
