using Nest;

namespace KariyerAnalytics.Data
{
    public class EFRepository : IRepository
    {
        private readonly ElasticsearchContext _ElasticsearchContext;

        public EFRepository()
        {
            _ElasticsearchContext = new ElasticsearchContext();
        }

        public void Add<T>(string indexname, T entity) where T : class
        {
            _ElasticsearchContext.Index(indexname, entity);
        }

        public ISearchResponse<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            return _ElasticsearchContext.Search<T>(searchRequest);
        }
    }
}
    
