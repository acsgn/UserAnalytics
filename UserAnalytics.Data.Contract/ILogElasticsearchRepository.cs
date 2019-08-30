using System.Collections.Generic;
using UserAnalytics.Business.Entities;
using Nest;

namespace UserAnalytics.Data.Contract
{
    public interface ILogElasticsearchRepository
    {
        bool Index(Log log);
        bool BulkIndex(IEnumerable<Log> documents);
        ISearchResponse<Log> Search(ISearchRequest searchRequest);
        ISuggestResponse Suggest(ISuggestRequest suggestRequest);
        ICountResponse Count(ICountRequest countRequest);
        void CreateIndex();
    }
}