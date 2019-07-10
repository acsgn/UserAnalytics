using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void Search<T>() where T : class
        {
            _ElasticsearchContext.Search<T>();
        }

        Task<IEnumerable<T>> IRepository.Search<T>()
        {
            throw new NotImplementedException();
        }
    }
}
    
