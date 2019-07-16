using Nest;

namespace KariyerAnalytics.Data
{
    public interface IRepository
    {
        void Add<T>(string indexname, T entity) where T : class;
        AggregationsHelper Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
