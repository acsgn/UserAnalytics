using System.Web.Http;
using UserAnalytics.Service.Entities;
using UserAnalytics.Business.Contract;

namespace UserAnalytics.Controllers
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
        public RealtimeUserCountResponseDTO[] GetEndpointsRealtimeUserCount(RealtimeRequest realtimeUserCountRequest)
        {
            return _RealtimeEngine.GetEndpointsRealtimeUserCount(realtimeUserCountRequest);
        }
    }
}
