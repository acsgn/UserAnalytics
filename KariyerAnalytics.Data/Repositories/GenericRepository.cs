using System;
using KariyerAnalytics.Common;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class GenericRepository<T> : Contract.IRepository<T>, IDisposable where T : class
    {
        public async void Index(string indexName, T document)
        {
            using (var context = new ElasticsearchContext())
            {
                await context.ElasticClient.IndexAsync(document, i => i.Index(indexName).Type<T>());
            }
        }

        public ISearchResponse<T> Search(ISearchRequest searchRequest)
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(searchRequest, context.ElasticClient);
                var searchResponse = context.ElasticClient.Search<T>(searchRequest);
                return searchResponse;
            }
        }

        public void CreateIndex(string indexName)
        {
            using (var context = new ElasticsearchContext())
            {
                if (context.ElasticClient.IndexExists(indexName).Exists)
                {
                    throw new Exception("The index is available, unable to create mapping!");
                }

                var createIndexResult = context.ElasticClient
                    .CreateIndex(indexName, indexDescriptor => indexDescriptor
                        .Mappings(mappingsDescriptor => mappingsDescriptor
                            .Map<T>(m => m.AutoMap())
                        )
                );

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
