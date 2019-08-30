using System.Collections.Generic;
using UserAnalytics.Business.Entities;

namespace UserAnalytics.Business.Contract
{
    public interface ILogElasticsearchEngine : IEngine
    {
        bool Add(Log log);
        bool AddMany(IEnumerable<Log> logs);
        void CreateIndex();
    }
}
