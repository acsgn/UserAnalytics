using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ILogElasticsearchEngine : IEngine
    {
        bool Add(Log log);
        void AddMany(IEnumerable<LogRequest> logRequests);
        void CreateIndex();
    }
}
