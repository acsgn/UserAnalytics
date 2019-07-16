using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data
{
    public interface IElasticsearchContext
    {
        void CreateIndex<T>(string indexName) where T : class;
        void Index<T>(string indexName, T document) where T : class;
        AggregationsHelper Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
