using System;
using System.Linq;
using KariyerAnalytics.Business.Entities;
using Nest;

namespace KariyerAnalytics.Data.Repositories
{
    public class CompanyRepository
    {
        public string[] GetCompanies(DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var companiesRequest = new SearchDescriptor<Log>()
                .Size(0)
                .Query(q => q
                    .DateRange(r => r
                        .Field(f => f.Timestamp)
                        .GreaterThanOrEquals(after)
                        .LessThanOrEquals(before)))
                .Aggregations(aggs => aggs
                    .Terms("companies", s => s
                        .Field(f => f.CompanyName)));

                var companiesResult = repository.Search(companiesRequest);

                var companyList = (from b in companiesResult.Aggs.Terms("companies").Buckets select b.Key).ToArray();

                return companyList;
            }
        }
        public string[] GetCompanyUsers(string companyName, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var usersRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .MatchPhrase(s => s
                        .Field(f => f.CompanyName)
                        .Query(companyName)))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("users", s => s
                        .Field(f => f.Username)));

                var usersResult = repository.Search(usersRequest);

                var userList = (from b in usersResult.Aggs.Terms("users").Buckets select b.Key).ToArray();

                return userList;
            }
        }
        public string[] GetEndpointsbyUserandCompany(string companyName, string username, DateTime after, DateTime before)
        {
            using (var repository = new GenericRepository<Log>())
            {
                var endpointsRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f.CompanyName)
                                .Query(companyName)),
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f.Username)
                                .Query(username)),
                            mu => mu
                            .DateRange(r => r
                                .Field(f => f.Timestamp)
                                .GreaterThanOrEquals(after)
                                .LessThanOrEquals(before)))))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f
                            .Endpoint)));

                var endpointsResult = repository.Search(endpointsRequest);

                var endpointsList = (from b in endpointsResult.Aggs.Terms("actions").Buckets select b.Key).ToArray();

                return endpointsList;
            }
        }

    }
}
