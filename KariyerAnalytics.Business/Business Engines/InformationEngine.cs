using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;

namespace KariyerAnalytics.Business
{
    public class InformationEngine : IInformationEngine
    {
        private readonly IInformationRepository _InformationRepository;

        public InformationEngine(IInformationRepository informationRepository)
        {
            _InformationRepository = informationRepository;
        }
        public string[] GetCompanies(InformationRequest request)
        {
            return _InformationRepository.GetCompanies(request.After, request.Before);
        }
        public string[] GetCompanyUsers(InformationRequest request)
        {
            return _InformationRepository.GetCompanyUsers(request.CompanyName, request.After, request.Before);
        }
        public string[] GetEndpoints(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.After, request.Before);
        }
        public string[] GetEndpointsByCompany(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.After, request.Before, request.CompanyName);
        }
        public string[] GetEndpointsByCompanyAndUser(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.After, request.Before, request.CompanyName, request.Username);
        }
    }
}
