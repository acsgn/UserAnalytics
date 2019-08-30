using System;
using System.Collections.Generic;
using System.IO;
using UserAnalytics.Data.Contract;
using Nest;

namespace UserAnalytics.Data.Repositories
{
    public class GenericElasticsearchRepository<T> : IGenericElasticsearchRepository<T> where T : class
    {
        public bool Index(string indexName, T document)
        {
            var result = ElasticsearchContext.ElasticClient.Index(document, i => i.Index(indexName).Type<T>());
            return result.Created;
        }
        public bool BulkIndex(string indexName, IEnumerable<T> documents)
        {
            var result = ElasticsearchContext.ElasticClient.IndexMany(documents, indexName);
            return !result.Errors;
        }

        public ISearchResponse<T> Search(ISearchRequest searchRequest)
        {
            var json = GetQueryJSonFromRequest(searchRequest, ElasticsearchContext.ElasticClient);
            var searchResponse = ElasticsearchContext.ElasticClient.Search<T>(searchRequest);
            return searchResponse;
        }

        public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
        {
            var json = GetQueryJSonFromRequest(suggestRequest, ElasticsearchContext.ElasticClient);
            var suggestResponse = ElasticsearchContext.ElasticClient.Suggest(suggestRequest);
            return suggestResponse;
        }

        public ICountResponse Count(ICountRequest countRequest)
        {
            var json = GetQueryJSonFromRequest(countRequest, ElasticsearchContext.ElasticClient);
            var countResponse = ElasticsearchContext.ElasticClient.Count<T>(countRequest);
            return countResponse;
        }

        public void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest)
        {
            if (ElasticsearchContext.ElasticClient.IndexExists(indexName).Exists)
            {
                throw new Exception("The index is available, unable to create index!");
            }

            var json = GetQueryJSonFromRequest(createIndexRequest, ElasticsearchContext.ElasticClient);
            var createIndexResult = ElasticsearchContext.ElasticClient.CreateIndex(createIndexRequest);

            if (!createIndexResult.IsValid || !createIndexResult.Acknowledged)
            {
                throw new Exception("Error on mapping!");
            }
        }

        public static SearchBuilder<T> CreateSearchBuilder(string indexName)
        {
            return new SearchBuilder<T>(indexName);
        }

        public static CountBuilder<T> CreateCountBuilder(string indexName)
        {
            return new CountBuilder<T>(indexName);
        }

        public static QueryBuilder<T> CreateQueryBuilder()
        {
            return new QueryBuilder<T>();
        }

        public static AggregationBuilder<T> CreateAggregationBuilder()
        {
            return new AggregationBuilder<T>();
        }

        private static string GetQueryJSonFromRequest(IRequest request, ElasticClient elasticClient)
        {
            using (var stream = new MemoryStream())
            {
                elasticClient.Serializer.Serialize(request, stream);
                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
