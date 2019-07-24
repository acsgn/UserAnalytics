using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        public string[] GetCompanies(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var companiesRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Filter("filtered", fi => fi
                        .Filter(fil => fil
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))
                        .Aggregations(nestedAggs => nestedAggs
                            .Terms("companies", s => s
                                .Field(f => f.CompanyName)))));

                var companiesResult = repository.Search(companiesRequest);

                var companyList = (from b in companiesResult.Aggs.Filter("filtered").Terms("companies").Buckets
                                   select b.Key).ToArray();

                return companyList;
            }
        }
        public string[] GetCompanyUsers(string companyName, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var usersRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Filter("filtered", fi => fi
                        .Filter(fil => fil
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))
                        .Aggregations(nestedAggs => nestedAggs
                            .Filter("filtered2", fi2 => fi2
                                .Filter(fil => fil
                                    .MatchPhrase(s => s
                                        .Field(f => f.CompanyName)
                                        .Query(companyName)))
                                .Aggregations(nestedNestedAggs => nestedNestedAggs                                
                                    .Terms("users", s => s
                                        .Field(f => f.Username)))))));

                var usersResult = repository.Search(usersRequest);

                var userList = (from b in usersResult.Aggs.Filter("filtered").Filter("filtered2").Terms("users").Buckets
                                select b.Key).ToArray();

                return userList;
            }
        }
        public DetailedMetricResponse[] GetEndpointMetricsbyCompany(string companyName, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var endpointsRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Filter("filtered", fi => fi
                        .Filter(fil => fil
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))
                        .Aggregations(aggs2 => aggs2
                            .Filter("filtered2", fi2 => fi2
                                .Filter(fil => fil
                                    .MatchPhrase(s => s
                                        .Field(f => f.CompanyName)
                                        .Query(companyName)))
                                .Aggregations(aggs3 => aggs3
                                    .Terms("endpoints", s => s
                                        .Field(f => f.Endpoint)
                                        .Aggregations(aggs5 => aggs5
                                            .Min("min-response-time", a => a.Field(f => f.ResponseTime))
                                            .Average("average-response-time", a => a.Field(f => f.ResponseTime))
                                            .Max("max-response-time", a => a.Field(f => f.ResponseTime))
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

                var endpointsResult = repository.Search(endpointsRequest);

                var buckets = endpointsResult.Aggs.Filter("filtered").Filter("filtered2").Terms("endpoints").Buckets;

                var endpointsList = (from b in buckets
                                     select new DetailedMetricResponse
                                     {
                                         Endpoint = b.Key,
                                         NumberOfRequests = (long)b.DocCount,
                                         MinResponseTime = (double)b.Min("min-response-time").Value,
                                         AverageResponseTime = (double)b.Average("average-response-time").Value,
                                         MaxResponseTime = (double)b.Max("max-response-time").Value,
                                     }).ToArray();

                return endpointsList;
            }
        }
        public DetailedMetricResponse[] GetEndpointMetricsbyUserandCompany(string companyName, string username, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var endpointsRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Filter("filtered", fi => fi
                        .Filter(fil => fil
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))
                        .Aggregations(aggs2 => aggs2
                            .Filter("filtered2", fi2 => fi2
                                .Filter(fil => fil
                                    .MatchPhrase(s => s
                                        .Field(f => f.CompanyName)
                                        .Query(companyName)))
                                .Aggregations(aggs3 => aggs3
                                    .Filter("filtered3", fi3 => fi3
                                        .Filter(fil => fil
                                            .MatchPhrase(s => s
                                                .Field(f => f.Username)
                                                .Query(username)))
                                        .Aggregations(aggs4 => aggs4
                                            .Terms("endpoints", s => s
                                                .Field(f => f.Endpoint)
                                                .Aggregations(aggs5 => aggs5
                                                    .Min("min-response-time", a => a
                                                        .Field(f => f.ResponseTime))
                                                    .Average("average-response-time", a => a
                                                        .Field(f => f.ResponseTime))
                                                    .Max("max-response-time", a => a
                                                        .Field(f => f.ResponseTime)))))))))));

                var endpointsResult = repository.Search(endpointsRequest);

                var buckets = endpointsResult.Aggs.Filter("filtered").Filter("filtered2").Filter("filtered3").Terms("endpoints").Buckets;

                var endpointsList = (from b in buckets
                                     select new DetailedMetricResponse
                                     {
                                         Endpoint = b.Key,
                                         NumberOfRequests = (long) b.DocCount,
                                         MinResponseTime = (double)b.Min("min-response-time").Value,
                                         AverageResponseTime = (double)b.Average("average-response-time").Value,
                                         MaxResponseTime = (double)b.Max("max-response-time").Value,
                                     }).ToArray();

                return endpointsList;
            }
        }

    }
}
