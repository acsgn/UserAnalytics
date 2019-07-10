using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data
{
    public interface IElasticsearchContext
    {
        void CreateIndex<T>(string indexName) where T : class;
        void Index<T>(string indexName, T document) where T : class;
        Task<IEnumerable<T>> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
