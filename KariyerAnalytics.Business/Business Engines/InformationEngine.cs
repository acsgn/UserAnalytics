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
        public string[] GetEndpoints(Request request)
        {
            return _InformationRepository.GetEndpoints(request.After, request.Before);
        }
        public string[] GetCompanies(Request request)
        {
            return _InformationRepository.GetCompanies(request.After, request.Before);
        }
        public string[] GetCompanyUsers(UserRequest userRequest)
        {
            return _InformationRepository.GetCompanyUsers(userRequest.CompanyName, userRequest.After, userRequest.Before);
        }

    }
}
