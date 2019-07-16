using System;
using System.IO;
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
                indexName, indexDescriptor => indexDescriptor
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

        public ISearchResponse<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            var json = GetQueryJSonFromRequest(searchRequest, _ElasticClient);
            var searchResponse = _ElasticClient.Search<T>(searchRequest);
            return searchResponse;
        }

        public static string GetQueryJSonFromRequest(ISearchRequest request, ElasticClient elasticClient)
        {
            using (var stream = new MemoryStream())
            {
                elasticClient.Serializer.Serialize(request, stream);
                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

    }

}
