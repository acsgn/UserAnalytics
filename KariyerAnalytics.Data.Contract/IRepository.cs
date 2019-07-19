using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface IRepository
    {
        void Index<T>(string indexName, T document) where T : class;
        ISearchResponse<T> Search<T>(ISearchRequest searchRequest) where T : class;
        void CreateIndex<T>(string indexName) where T : class;
    }
}
