using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface IInformationRepository
    {
        string[] GetCompanies(string endpoint, string username, DateTime after, DateTime before);
        string[] GetUsers(string endpoint, string companyName, DateTime after, DateTime before);
        string[] GetEndpoints(string companyName, string username, DateTime after, DateTime before);
    }
}