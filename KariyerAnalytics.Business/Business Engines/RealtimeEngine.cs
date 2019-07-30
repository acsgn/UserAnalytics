﻿using System.Linq;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business
{
    public class RealtimeEngine : IRealtimeEngine
    {
        private readonly IRealtimeRepository _RealtimeRepository;

        public RealtimeEngine(IRealtimeRepository realtimeRepository)
        {
            _RealtimeRepository = realtimeRepository;
        }

        public long GetRealtimeUserCount(RealtimeRequest realtimeUserCountRequest)
        {
            return _RealtimeRepository.GetRealtimeUserCount(realtimeUserCountRequest.SecondsBefore);
        }

        public RealtimeUserCountResponseDTO[] GetEndpointsRealtimeUserCount(RealtimeRequest request)
        {
            var result = _RealtimeRepository.GetEndpointsRealtimeUserCount(request.SecondsBefore, request.Size);
            return (from r in result
                    select new RealtimeUserCountResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        UserCount = r.UserCount
                    }).ToArray();
        }
    }
}
