using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business.Contract
{
    public interface IInformationEngine : IEngine
    {
        string[] GetCompanies(InformationRequest request);
        string[] GetUsers(InformationRequest userDetailRequest);
        string[] GetEndpoints(InformationRequest request);
    }
}
