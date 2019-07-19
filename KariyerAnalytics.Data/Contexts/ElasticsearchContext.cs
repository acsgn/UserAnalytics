using System;
using System.IO;
using KariyerAnalytics.Common;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data
{
    public class ElasticsearchContext : IElasticsearchContext, IDisposable
    {
        public readonly ElasticClient ElasticClient;
        public ElasticsearchContext()
        {
            ElasticClient = new ElasticClient(ElasticsearchConnectionSettingsSingleton.GetDefaultConnectionSettings());
        }

        public void Dispose()
        {
            GC.Collect();
        } 
    }

}
