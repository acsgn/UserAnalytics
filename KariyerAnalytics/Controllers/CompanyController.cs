using System.Web.Http;
using KariyerAnalytics.Business;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class CompanyController : ApiController
    {
        [HttpGet]
        public string[] GetCompanies()
        {
            var engine = new CompanyEngine();
            return engine.GetCompanies(new Request());
        }

        [HttpGet]
        public string[] GetCompanyUsers(CompanyDetailRequest companyDetailRequest)
        {
            var engine = new CompanyEngine();
            return engine.GetCompanyUsers(companyDetailRequest);
        }

        [HttpGet]
        public string[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            var engine = new CompanyEngine();
            return engine.GetEndpointsbyUserandCompany(userDetailRequest);
        }
    }
}
