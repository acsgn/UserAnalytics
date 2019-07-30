using KariyerAnalytics.Business.Entities;
using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogElasticsearchRepository
    {
        void Index(Log log);
        ISearchResponse<Log> Search(ISearchRequest searchRequest);
        ISuggestResponse Suggest(ISuggestRequest suggestRequest);
        ICountResponse Count(ICountRequest countRequest);
        void CreateIndex();
    }
}