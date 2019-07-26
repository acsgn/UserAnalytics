using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogElasticsearchRepository
    {
        void CreateIndex();
        void Index(Log log);
    }
}