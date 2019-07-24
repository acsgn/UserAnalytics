using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyEngine _CompanyEngine;

        public CompanyController(ICompanyEngine companyEngine)
        {
            _CompanyEngine = companyEngine;
        }

        [HttpGet]
        public string[] GetCompanies()
        {
            return _CompanyEngine.GetCompanies(new Request());
        }

        [HttpGet]
        public string[] GetCompanyUsers(CompanyDetailRequest companyDetailRequest)
        {
            return _CompanyEngine.GetCompanyUsers(companyDetailRequest);
        }

        [HttpGet]
        public DetailedMetricResponseDTO[] GetEndpointMetricsbyCompany(CompanyDetailRequest companyDetailRequest)
        {
            return _CompanyEngine.GetEndpointMetricsbyCompany(companyDetailRequest);
        }

        [HttpGet]
        public DetailedMetricResponseDTO[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            return _CompanyEngine.GetEndpointsbyUserandCompany(userDetailRequest);
        }
    }
}
