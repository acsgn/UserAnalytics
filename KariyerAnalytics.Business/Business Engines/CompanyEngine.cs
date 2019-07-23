using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Business
{
    public class CompanyEngine : ICompanyEngine
    {
        private CompanyRepository _CompanyRepository;

        public CompanyEngine(CompanyRepository companyRepository)
        {
            _CompanyRepository = companyRepository;
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
