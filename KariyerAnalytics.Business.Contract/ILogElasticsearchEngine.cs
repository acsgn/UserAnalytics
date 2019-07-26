using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogElasticsearchEngine : IEngine
    {
        void Add(LogRequest logRequest);
        void CreateIndex();
    }
}
