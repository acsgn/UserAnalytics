using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        public string[] GetEndpoints(DateTime after, DateTime before)
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
                            .Aggregations(nestedAggs => nestedAggs
                                .Terms("endpoints", s => s
                                    .Field(f => f.Endpoint)))));

                var endpointsResult = repository.Search(endpointsRequest);

                var buckets = endpointsResult.Aggs.Filter("filtered").Terms("endpoints").Buckets;

                var endpointList = (from b in buckets select b.Key).ToArray();

                return endpointList;
            }
        }
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

                var buckets = usersResult.Aggs.Filter("filtered").Filter("filtered2").Terms("users").Buckets;

                var userList = (from b in buckets select b.Key).ToArray();

                return userList;
            }
        }
    }
}
