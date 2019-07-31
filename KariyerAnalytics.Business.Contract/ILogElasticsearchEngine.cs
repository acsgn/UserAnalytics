using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogElasticsearchEngine : IEngine
    {
        bool Add(Log log);
        bool AddMany(IEnumerable<Log> logs);
        void CreateIndex();
    }
}
