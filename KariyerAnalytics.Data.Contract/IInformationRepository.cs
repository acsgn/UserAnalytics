using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface IInformationRepository
    {
        string[] GetCompanies(DateTime after, DateTime before);
        string[] GetCompanyUsers(string companyName, DateTime after, DateTime before);
        string[] GetEndpoints(DateTime after, DateTime before, string companyName = null, string username = null);
    }
}