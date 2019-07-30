using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface IGenericElasticsearchRepository<T> where T : class
    {
        void Index(string indexName, T document);
        void BulkIndex(string indexName, IEnumerable<T> documents);
        ISearchResponse<T> Search(ISearchRequest searchRequest);
        ISuggestResponse Suggest(ISuggestRequest suggestRequest);
        ICountResponse Count(ICountRequest countRequest);
        void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest);
    }
}
