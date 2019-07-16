using Nest;

namespace KariyerAnalytics.Data
{
    public interface IRepository
    {
        void Add<T>(string indexname, T entity) where T : class;
        ISearchResponse<T> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
