﻿using System;
using System.Collections.Generic;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class LogElasticsearchRepository : ILogElasticsearchRepository, IDisposable
    {
        private readonly static string _IndexName = "logs";
        public void Index(Log log)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                repository.Index(_IndexName, log);
            }
        }
        public void BulkIndex(IEnumerable<Log> documents)
        {
            using (var repository = new GenericElasticsearchRepository<Log>())
            {
                repository.BulkIndex(_IndexName, documents);
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
                ICreateIndexRequest createIndexRequest = new CreateIndexDescriptor(_IndexName)
                    .Mappings(m => m
                        .Map<Log>(map => map
                            .AutoMap()
                            .Properties(p => p
                                .String(s => s
                                    .Name(n => n.CompanyName)))));
                repository.CreateIndex(_IndexName, createIndexRequest);
            }
        }

        public SearchBuilder<Log> CreateSearchBuilder()
        {
            return new SearchBuilder<Log>(_IndexName);
        }

        public CountBuilder<Log> CreateCountBuilder()
        {
            return new CountBuilder<Log>(_IndexName);
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
