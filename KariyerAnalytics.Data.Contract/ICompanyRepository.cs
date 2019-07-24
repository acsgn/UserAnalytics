using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ICompanyRepository
    {
        string[] GetCompanies(DateTime after, DateTime before);
        string[] GetCompanyUsers(string companyName, DateTime after, DateTime before);
        DetailedMetricResponse[] GetEndpointMetricsbyCompany(string companyName, DateTime after, DateTime before);
        DetailedMetricResponse[] GetEndpointMetricsbyUserandCompany(string companyName, string username, DateTime after, DateTime before);
    }
}