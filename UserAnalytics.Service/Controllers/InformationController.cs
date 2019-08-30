using System.Web.Http;
using UserAnalytics.Business.Contract;
using UserAnalytics.Service.Entities;

namespace UserAnalytics.Controllers
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
