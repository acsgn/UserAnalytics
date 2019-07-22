using System;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data
{
    public class ElasticsearchContext : IElasticsearchContext, IDisposable
    {
        private readonly ElasticClient _ElasticClient;
        public ElasticsearchContext()
        {
            _ElasticClient = new ElasticClient(ElasticsearchConnectionSettingsSingleton.GetDefaultConnectionSettings());
        }

        public ElasticClient GetElasticClient()
        {
            return _ElasticClient;
        }

        public void Dispose()
        {
            GC.Collect();
        } 
    }

}
