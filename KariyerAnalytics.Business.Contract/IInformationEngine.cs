using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IInformationEngine : IEngine
    {
        string[] GetEndpoints(Request request);
        string[] GetCompanies(Request request);
        string[] GetCompanyUsers(UserRequest userDetailRequest);
    }
}
