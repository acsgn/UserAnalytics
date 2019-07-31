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
            return _InformationRepository.GetCompanies(request.Endpoint, request.CompanyName, request.Username);
        }
        public string[] GetUsers(InformationRequest request)
        {
            return _InformationRepository.GetUsers(request.Endpoint, request.CompanyName, request.Username);
        }
        public string[] GetEndpoints(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.Endpoint, request.CompanyName, request.Username);
        }
    }
}
