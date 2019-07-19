using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface IRepository<T> where T : class
    {
        void Index(string indexName, T document);
        ISearchResponse<T> Search(ISearchRequest searchRequest);
        void CreateIndex(string indexName);
    }
}
