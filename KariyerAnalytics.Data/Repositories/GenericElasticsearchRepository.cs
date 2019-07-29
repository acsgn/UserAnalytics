using System;
using KariyerAnalytics.Common;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericElasticsearchRepository<T> : IGenericElasticsearchRepository<T>, IDisposable where T : class
    {
        public async void Index(string indexName, T document)
        {
            using (var context = new ElasticsearchContext())
            {
                await context.GetElasticClient().IndexAsync(document, i => i.Index(indexName).Type<T>());
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

        public ICountResponse Count(ICountRequest countRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(countRequest, context.GetElasticClient());
                var countResponse = context.GetElasticClient().Count<T>(countRequest);
                return countResponse;
            }
        }

        public void CreateIndex(string indexName)
        {
            using (var context = new ElasticsearchContext())
            {
                if (context.GetElasticClient().IndexExists(indexName).Exists)
                {
                    throw new Exception("The index is available, unable to create mapping!");
                }

                var indexDescriptor = new CreateIndexDescriptor(indexName)
                    .Mappings(m => m
                        .Map<T>(map => map.AutoMap()));

                var createIndexResult = context.GetElasticClient().CreateIndex(indexDescriptor);

                if (!createIndexResult.IsValid || !createIndexResult.Acknowledged)
                {
                    throw new Exception("Error on mapping!");
                }
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }

    }
}
