using System;
using System.Collections.Generic;
using KariyerAnalytics.Common;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericElasticsearchRepository<T> : IGenericElasticsearchRepository<T>, IDisposable where T : class
    {
        public bool Index(string indexName, T document)
        {
            using (var context = new ElasticsearchContext())
            {
                var result = context.GetElasticClient().Index(document, i => i.Index(indexName).Type<T>());
                return result.Created;
            }
        }
        public bool BulkIndex(string indexName, IEnumerable<T> documents)
        {
            using (var context = new ElasticsearchContext())
            {
                var result = context.GetElasticClient().IndexMany(documents, indexName);
                return !result.Errors;
            }
        }

        public ISearchResponse<T> Search(ISearchRequest searchRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(searchRequest, context.GetElasticClient());
                var searchResponse = context.GetElasticClient().Search<T>(searchRequest);
                return searchResponse;
            }
        }

        public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(suggestRequest, context.GetElasticClient());
                var suggestResponse = context.GetElasticClient().Suggest(suggestRequest);
                return suggestResponse;
            }
        }

        public ICountResponse Count(ICountRequest countRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(countRequest, context.GetElasticClient());
                var countResponse = context.GetElasticClient().Count<T>(countRequest);
                return countResponse;
            }
        }

        public void CreateIndex(string indexName, ICreateIndexRequest createIndexRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                if (context.GetElasticClient().IndexExists(indexName).Exists)
                {
                    throw new Exception("The index is available, unable to create index!");
                }

                var createIndexResult = context.GetElasticClient().CreateIndex(createIndexRequest);

                if (!createIndexResult.IsValid || !createIndexResult.Acknowledged)
                {
                    throw new Exception("Error on mapping!");
                }
            }
        }

        public SearchBuilder<T> CreateSearchBuilder(string indexName)
        {
            return new SearchBuilder<T>(indexName);
        }

        public CountBuilder<T> CreateCountBuilder(string indexName)
        {
            return new CountBuilder<T>(indexName);
        }

        public QueryBuilder<T> CreateQueryBuilder()
        {
            return new QueryBuilder<T>();
        }

        public AggregationBuilder<T> CreateAggregationBuilder()
        {
            return new AggregationBuilder<T>();
        }

        public void Dispose()
        {
            GC.Collect();
        }

    }
}
