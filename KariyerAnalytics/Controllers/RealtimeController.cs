using System.Web.Http;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Business.Contract;

namespace KariyerAnalytics.Controllers
{
    public class RealtimeController : ApiController
    {
        private readonly IRealtimeEngine _RealtimeEngine;

        public RealtimeController(IRealtimeEngine realtimeEngine)
        {
            _RealtimeEngine = realtimeEngine;
        }

        [HttpGet]
        public long GetRealtimeUserCount(RealtimeRequest realtimeUserCountRequest)
        {
            return _RealtimeEngine.GetRealtimeUserCount(realtimeUserCountRequest);
        }

        [HttpGet]
        public RealtimeUserCountResponseDTO[] GetRealtimeUserCountByEndpoints(RealtimeRequest realtimeUserCountRequest)
        {
            return _RealtimeEngine.GetRealtimeUserCountByEndpoints(realtimeUserCountRequest);
        }
    }
}
