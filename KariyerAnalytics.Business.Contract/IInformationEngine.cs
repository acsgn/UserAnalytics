using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IInformationEngine : IEngine
    {
        string[] GetCompanies(InformationRequest request);
        string[] GetCompanyUsers(InformationRequest userDetailRequest);
        string[] GetEndpoints(InformationRequest request);
        string[] GetEndpointsByCompany(InformationRequest request);
        string[] GetEndpointsByCompanyAndUser(InformationRequest request);
    }
}
