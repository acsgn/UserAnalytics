using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface ICompanyRepository
    {
        string[] GetCompanies(DateTime after, DateTime before);
        string[] GetCompanyUsers(string companyName, DateTime after, DateTime before);
        string[] GetEndpointsbyUserandCompany(string companyName, string username, DateTime after, DateTime before);
    }
}