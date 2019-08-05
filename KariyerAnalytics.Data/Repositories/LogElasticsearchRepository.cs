using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogElasticsearchRepository : ILogElasticsearchRepository
    {
        private readonly static string _IndexName = "logs";

        private IGenericElasticsearchRepository<Log> _ElasticsearchRepository;

        public LogElasticsearchRepository(IGenericElasticsearchRepository<Log> repository)
        {
            _ElasticsearchRepository = repository;
        }
        public bool Index(Log log)
        {
            return _ElasticsearchRepository.Index(_IndexName, log);
        }
        public bool BulkIndex(IEnumerable<Log> documents)
        {
            return _ElasticsearchRepository.BulkIndex(_IndexName, documents);
        }
        public ISearchResponse<Log> Search(ISearchRequest searchRequest)
        {
            return _ElasticsearchRepository.Search(searchRequest);
        }
        public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
        {
            return _ElasticsearchRepository.Suggest(suggestRequest);
        }
        public ICountResponse Count(ICountRequest countRequest)
        {
            return _ElasticsearchRepository.Count(countRequest);
        }
        public void CreateIndex()
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

            _ElasticsearchRepository.CreateIndex(_IndexName, createIndexRequest);
        }

        public static SearchBuilder<Log> CreateSearchBuilder()
        {
            return GenericElasticsearchRepository<Log>.CreateSearchBuilder(_IndexName);
        }

        public static CountBuilder<Log> CreateCountBuilder()
        {
            return GenericElasticsearchRepository<Log>.CreateCountBuilder(_IndexName);
        }

        public static QueryBuilder<Log> CreateQueryBuilder()
        {
            return GenericElasticsearchRepository<Log>.CreateQueryBuilder();
        }

        public static AggregationBuilder<Log> CreateAggregationBuilder()
        {
            return GenericElasticsearchRepository<Log>.CreateAggregationBuilder();
        }
    }
}
