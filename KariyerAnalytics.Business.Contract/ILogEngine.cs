using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogEngine : IEngine
    {
        void Add(LogInformation info);
        void CreateIndex();
    }
}
