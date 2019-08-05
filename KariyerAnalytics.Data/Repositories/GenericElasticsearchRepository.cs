using System;
using System.Collections.Generic;
using System.IO;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericElasticsearchRepository<T> : IGenericElasticsearchRepository<T> where T : class
    {
        private IElasticsearchContext _ElasticsearchContext;
        public GenericElasticsearchRepository(IElasticsearchContext context)
        {
            _ElasticsearchContext = context;
        }
        public bool Index(string indexName, T document)
        {
            var result = _ElasticsearchContext.GetElasticClient().Index(document, i => i.Index(indexName).Type<T>());
            return result.Created;
        }
        public bool BulkIndex(string indexName, IEnumerable<T> documents)
        {
            var result = _ElasticsearchContext.GetElasticClient().IndexMany(documents, indexName);
            return !result.Errors;
        }

        public ISearchResponse<T> Search(ISearchRequest searchRequest)
        {
            var json = GetQueryJSonFromRequest(searchRequest, _ElasticsearchContext.GetElasticClient());
            var searchResponse = _ElasticsearchContext.GetElasticClient().Search<T>(searchRequest);
            return searchResponse;
        }

        public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
        {
            var json = GetQueryJSonFromRequest(suggestRequest, _ElasticsearchContext.GetElasticClient());
            var suggestResponse = _ElasticsearchContext.GetElasticClient().Suggest(suggestRequest);
            return suggestResponse;
        }

        public ICountResponse Count(ICountRequest countRequest)
        {
            var json = GetQueryJSonFromRequest(countRequest, _ElasticsearchContext.GetElasticClient());
            var countResponse = _ElasticsearchContext.GetElasticClient().Count<T>(countRequest);
            return countResponse;
        }

        public void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest)
        {
            if (_ElasticsearchContext.GetElasticClient().IndexExists(indexName).Exists)
            {
                throw new Exception("The index is available, unable to create index!");
            }

            var json = GetQueryJSonFromRequest(createIndexRequest, _ElasticsearchContext.GetElasticClient());
            var createIndexResult = _ElasticsearchContext.GetElasticClient().CreateIndex(createIndexRequest);

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
