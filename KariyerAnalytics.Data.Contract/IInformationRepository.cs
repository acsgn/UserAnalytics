using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface IInformationRepository
    {
        string[] GetEndpoints(DateTime after, DateTime before);
        string[] GetCompanies(DateTime after, DateTime before);
        string[] GetCompanyUsers(string companyName, DateTime after, DateTime before);
    }
}