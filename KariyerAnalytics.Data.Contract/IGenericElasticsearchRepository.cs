using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface IGenericElasticsearchRepository<T> where T : class
    {
        void Index(string indexName, T document);
        ISearchResponse<T> Search(ISearchRequest searchRequest);
        ISuggestResponse Suggest(ISuggestRequest suggestRequest);
        ICountResponse Count(ICountRequest countRequest);
        void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest);
    }
}
