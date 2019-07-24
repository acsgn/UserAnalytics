using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;
using System.Linq;

namespace KariyerAnalytics.Business
{
    public class CompanyEngine : ICompanyEngine
    {
        private readonly ICompanyRepository _CompanyRepository;

        public CompanyEngine(ICompanyRepository companyRepository)
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
        public DetailedMetricResponseDTO[] GetEndpointMetricsbyCompany(CompanyDetailRequest companyDetailRequest)
        {
            var result = _CompanyRepository.GetEndpointMetricsbyCompany(companyDetailRequest.CompanyName, companyDetailRequest.After, companyDetailRequest.Before);
            return (from r in result
                    select new DetailedMetricResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }
        public DetailedMetricResponseDTO[] GetEndpointsbyUserandCompany(UserDetailRequest userDetailRequest)
        {
            var result = _CompanyRepository.GetEndpointMetricsbyUserandCompany(userDetailRequest.CompanyName, userDetailRequest.Username, userDetailRequest.After, userDetailRequest.Before);
            return (from r in result
                    select new DetailedMetricResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }
    }
}
