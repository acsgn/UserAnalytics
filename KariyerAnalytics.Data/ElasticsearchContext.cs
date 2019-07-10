using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data
{
    public class ElasticsearchContext : IElasticsearchContext
    {
        private readonly ElasticClient _ElasticClient;
        public ElasticsearchContext()
        {
            _ElasticClient = new ElasticClient(DefaultConnectionSettings.GetConnectionSettings());
        }

        public void CreateIndex<T>(string indexName) where T : class
        {
            if (_ElasticClient.IndexExists(indexName).Exists)
            {
                throw new Exception("The index is available, unable to create mapping!");
            }


            var createIndexResult = _ElasticClient.CreateIndex(
                indexName, createIndexDescriptor => createIndexDescriptor
                    .Mappings(mappingsDescriptor => mappingsDescriptor
                        .Map<T>(m => m.AutoMap())
                    )
            );


            if (!createIndexResult.IsValid || !createIndexResult.Acknowledged)
            {
                throw new Exception("Error on mapping!");
            }
        }

        public void Index<T>(string indexName, T document) where T : class
        {
            _ElasticClient.Index(document, i => i.Index(indexName).Type<T>());
        }

        public ICollection<T> Search<T>() where T : class
        {
            var request = (new SearchDescriptor<T>()).Aggregations(agg => agg
                .Terms("urls", e => e.Field("uRL")
                .Aggregations(agg2 => agg2.Average("avgs", e2 => e2.Field("responseTime"))))
                );

            GetLastExecutedQueryJSonFromRequest(request, _ElasticClient);

            var response = _ElasticClient.Search<T>(request);
            //var response = await _ElasticClient.SearchAsync<T>(searchRequest);

            //ResolveAverageResponseTimeAggregationBucket(new AggregationsHelper(response.Aggregations));

            var result = response.Aggs.Terms("urls");
            var maxAge = result.Buckets;
            return null;
        }
        

        Task<IEnumerable<T>> IElasticsearchContext.Search<T>(ISearchRequest searchRequest)
        {
            throw new NotImplementedException();
        }

        public static string GetLastExecutedQueryJSonFromRequest(ISearchRequest request, ElasticClient elasticClient)
        {
            using (var stream = new MemoryStream())
            {
                elasticClient.Serializer.Serialize(request, stream);

                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

    }

}
