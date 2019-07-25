using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogEngine : IEngine
    {
        void Add(LogRequest logRequest);
        void CreateIndex();
    }
}
