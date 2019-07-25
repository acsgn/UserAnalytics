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
        public string[] GetEndpoints(Request request)
        {
            return _InformationEngine.GetEndpoints(request);
        }

        [HttpGet]
        public string[] GetCompanies(Request request)
        {
            return _InformationEngine.GetCompanies(request);
        }

        [HttpGet]
        public string[] GetCompanyUsers(UserRequest userRequest)
        {
            return _InformationEngine.GetCompanyUsers(userRequest);
        }
    }
}
