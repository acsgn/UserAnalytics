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
            return _InformationRepository.GetCompanies(request.Endpoint, request.Username, request.After, request.Before);
        }
        public string[] GetUsers(InformationRequest request)
        {
            return _InformationRepository.GetUsers(request.Endpoint, request.CompanyName, request.After, request.Before);
        }
        public string[] GetEndpoints(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.CompanyName, request.Username, request.After, request.Before);
        }
    }
}
