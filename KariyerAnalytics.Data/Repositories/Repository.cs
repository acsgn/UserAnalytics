using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerAnalytics.Common;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class Repository : IRepository
    {
        public async void Index<T>(string indexName, T document) where T : class
        {
            using (var context = new ElasticsearchContext())
            {
                await context.ElasticClient.IndexAsync(document, i => i.Index(indexName).Type<T>());
            }
        }

        public ISearchResponse<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            using (var context = new ElasticsearchContext())
            {
                var json = StringHelpers.GetQueryJSonFromRequest(searchRequest, context.ElasticClient);
                var searchResponse = context.ElasticClient.Search<T>(searchRequest);
                return searchResponse;
            }
        }

        public void CreateIndex<T>(string indexName) where T : class
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
    }
}
