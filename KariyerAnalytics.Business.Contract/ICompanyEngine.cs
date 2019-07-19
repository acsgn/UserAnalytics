using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ICompanyEngine : IEngine
    {
        string[] GetCompanies(Request request);
        string[] GetCompanyUsers(CompanyDetailRequest userDetailRequest);
        string[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest);
    }
}
