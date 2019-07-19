using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogEngine
    {
        void Add(LogInformation info);
        void CreateIndex();
    }
}
