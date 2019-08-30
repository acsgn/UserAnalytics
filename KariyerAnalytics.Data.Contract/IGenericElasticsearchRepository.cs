using System.Collections.Generic;
using Nest;

namespace UserAnalytics.Data.Contract
{
    public interface IGenericElasticsearchRepository<T> where T : class
    {
        bool Index(string indexName, T document);
        bool BulkIndex(string indexName, IEnumerable<T> documents);
        ISearchResponse<T> Search(ISearchRequest searchRequest);
        ISuggestResponse Suggest(ISuggestRequest suggestRequest);
        ICountResponse Count(ICountRequest countRequest);
        void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest);
    }
}
