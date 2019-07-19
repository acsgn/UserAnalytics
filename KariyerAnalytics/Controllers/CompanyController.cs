using System.ComponentModel.Composition;
using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Common.DependencyInjection;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyController : ApiController
    {
        [Import]
        private ICompanyEngine _CompanyEngine;

        public CompanyController()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
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
        public string[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            return _CompanyEngine.GetEndpointsbyUserandCompany(userDetailRequest);
        }
    }
}
