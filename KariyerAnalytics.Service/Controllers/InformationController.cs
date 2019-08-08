using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class InformationController : ApiController
    {
        private readonly IInformationEngine _InformationEngine;

        public InformationController(IInformationEngine InformationEngine)
        {
            _InformationEngine = InformationEngine;
        }

        [HttpGet]
        public string[] GetCompanies(InformationRequest request)
        {
            return _InformationEngine.GetCompanies(request);
        }

        [HttpGet]
        public string[] GetUsers(InformationRequest userRequest)
        {
            return _InformationEngine.GetUsers(userRequest);
        }

        [HttpGet]
        public string[] GetEndpoints(InformationRequest request)
        {
            return _InformationEngine.GetEndpoints(request);
        }
    }
}
