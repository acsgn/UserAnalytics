using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.ComponentModel.Composition;
using KariyerAnalytics.Common.DependencyInjection;

namespace KariyerAnalytics.Business
{
    [Export(typeof(ICompanyEngine))]
    public class CompanyEngine : ICompanyEngine
    {
        [Import]
        private CompanyRepository _CompanyRepository;

        public CompanyEngine()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }

        public string[] GetCompanies(Request request)
        {
            return _CompanyRepository.GetCompanies(request.After, request.Before);
        }
        public string[] GetCompanyUsers(CompanyDetailRequest companyDetailRequest)
        {
            return _CompanyRepository.GetCompanyUsers(companyDetailRequest.CompanyName, companyDetailRequest.After, companyDetailRequest.Before);
        }
        public string[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            return _CompanyRepository.GetEndpointsbyUserandCompany(userDetailRequest.CompanyName, userDetailRequest.Username, userDetailRequest.After, userDetailRequest.Before);
        }
    }
}
