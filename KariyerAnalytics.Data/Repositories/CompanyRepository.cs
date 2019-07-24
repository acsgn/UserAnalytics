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
        public string[] GetEndpointsbyUserandCompany(string companyName, string username, DateTime after, DateTime before)
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
                            .Filter("filtered2", fi2 => fi2
                                .Filter(fil => fil
                                    .MatchPhrase(s => s
                                        .Field(f => f.CompanyName)
                                        .Query(companyName)))
                                .Aggregations(nestedNestedAggs => nestedNestedAggs
                                    .Filter("filtered3", fi3 => fi3
                                        .Filter(fil => fil
                                            .MatchPhrase(s => s
                                                .Field(f => f.Username)
                                                .Query(username)))
                                        .Aggregations(nestedNestedNestedAggs => nestedNestedNestedAggs
                                            .Terms("endpoints", s => s
                                                .Field(f => f.Endpoint)))))))));

                var endpointsResult = repository.Search(endpointsRequest);

                var endpointsList = (from b in endpointsResult.Aggs.Filter("filtered").Filter("filtered2").Filter("filtered3").Terms("endpoints").Buckets
                                     select b.Key).ToArray();

                return endpointsList;
            }
        }

    }
}
