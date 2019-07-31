namespace KariyerAnalytics.Data.Contract
{
    public interface IInformationRepository
    {
        string[] GetCompanies(string endpoint, string companyName, string username, int? size);
        string[] GetUsers(string endpoint, string companyName, string username, int? size);
        string[] GetEndpoints(string endpoint, string companyName, string username, int? size);
    }
}