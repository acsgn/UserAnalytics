using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;

namespace KariyerAnalytics.Business
{
    public class CompanyEngine : ICompanyEngine
    {
        public string[] GetCompanies(Request request)
        {
            var companyRepository = new CompanyRepository();
            return companyRepository.GetCompanies(request.After, request.Before);
        }
        public string[] GetCompanyUsers(CompanyDetailRequest companyDetailRequest)
        {
            var companyRepository = new CompanyRepository();
            return companyRepository.GetCompanyUsers(companyDetailRequest.CompanyName, companyDetailRequest.After, companyDetailRequest.Before);
        }
        public string[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            var companyRepository = new CompanyRepository();
            return companyRepository.GetEndpointsbyUserandCompany(userDetailRequest.CompanyName, userDetailRequest.Username, userDetailRequest.After, userDetailRequest.Before);
        }
    }
}
