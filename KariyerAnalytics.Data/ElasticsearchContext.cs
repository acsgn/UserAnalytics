using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data
{
    public class ElasticsearchContext : IElasticsearchContext
    {
        private readonly ElasticClient _ElasticClient;
        public ElasticsearchContext()
        {
            _ElasticClient = new ElasticClient(ElasticsearchConnectionSettings.GetDefaultConnectionSettings());
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

        public AggregationsHelper Search<T>(ISearchRequest searchRequest) where T : class
        {
            //var request = (new SearchDescriptor<T>()).Size(0).Aggregations(agg => agg
            //    .Terms("urls", e => e.Field("uRL")
            //    .Aggregations(agg2 => agg2.Average("avgs", e2 => e2.Field("responseTime"))))
            //    );

            var response = _ElasticClient.Search<T>(q => q.Size(1).Sort(s => s.Descending("responseTime")));

            return response.Aggs;

            //var result = response.Aggs.Terms("urls").Buckets;
            //List<KeyValuePair<string, double>> output = new List<KeyValuePair<string, double>>();
            //foreach (KeyedBucket b in result)
            //{
            //    string key= b.Key;
            //    double value = 0;
            //    var d2 = b.Average("avgs").Value;
            //    if (d2.HasValue)
            //    { value = (double)d2; }
            //    KeyValuePair<string, double> pair = new KeyValuePair<string, double>(key,value);
            //    output.Add(pair);
            //}
            //return output;

        }

    }

}
