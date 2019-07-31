using System;

namespace KariyerAnalytics.Data.Contract
{
    public interface IInformationRepository
    {
        string[] GetCompanies(string endpoint, string companyName, string username);
        string[] GetUsers(string endpoint, string companyName, string username);
        string[] GetEndpoints(string endpoint, string companyName, string username);
    }
}