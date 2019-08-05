using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogElasticsearchRepository : ILogElasticsearchRepository, IDisposable
    {
        private readonly static string _IndexName = "logs";
        public bool Index(Log log)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.Index(_IndexName, log);
            }
        }
        public bool BulkIndex(IEnumerable<Log> documents)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.BulkIndex(_IndexName, documents);
            }
        }
        public ISearchResponse<Log> Search(ISearchRequest searchRequest)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.Search(searchRequest);
            }
        }
        public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.Suggest(suggestRequest);
            }
        }
        public ICountResponse Count(ICountRequest countRequest)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.Count(countRequest);
            }
        }
        public void CreateIndex()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                var createIndexRequest = new CreateIndexDescriptor(_IndexName)
                    .Mappings(m => m
                        .Map<Log>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .String(s => s
                                    .Name(n => n.CompanyName)
                                    .Analyzer("ngram_analyzer"))
                                .String(s => s
                                    .Name(n => n.Username)
                                    .Analyzer("ngram_analyzer"))
                                .String(s => s
                                    .Name(n => n.Endpoint)
                                    .Analyzer("ngram_analyzer"))
                                .Ip(i => i
                                    .Name(n => n.IP)))))
                    .Settings(s => s
                        .Analysis(f => f.Analyzers(a => a.Custom("ngram_analyzer", c => c.Tokenizer("ngram_tokenizer")))
                        .Tokenizers(t => t.EdgeNGram("ngram_tokenizer", n => n.MinGram(3).MaxGram(8)))));

                repository.CreateIndex(_IndexName, createIndexRequest);
            }
        }

        public SearchBuilder<Log> CreateSearchBuilder()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.CreateSearchBuilder(_IndexName);
            }
        }

        public CountBuilder<Log> CreateCountBuilder()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.CreateCountBuilder(_IndexName);
            }
        }

        public QueryBuilder<Log> CreateQueryBuilder()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.CreateQueryBuilder();
            }
        }

        public AggregationBuilder<Log> CreateAggregationBuilder()
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                return repository.CreateAggregationBuilder();
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
