using UserAnalytics.Business.Contract;
using UserAnalytics.Service.Entities;
using UserAnalytics.Data.Contract;

namespace UserAnalytics.Business
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
            return _InformationRepository.GetCompanies(request.Endpoint, request.CompanyName, request.Username, request.Size);
        }
        public string[] GetUsers(InformationRequest request)
        {
            return _InformationRepository.GetUsers(request.Endpoint, request.CompanyName, request.Username, request.Size);
        }
        public string[] GetEndpoints(InformationRequest request)
        {
            return _InformationRepository.GetEndpoints(request.Endpoint, request.CompanyName, request.Username, request.Size);
        }
    }
}
