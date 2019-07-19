using System.Collections.Generic;
using System.Linq;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data;
using KariyerAnalytics.Data.Repositories;
using Nest;

namespace KariyerAnalytics.Business
{
    public class CompanyEngine : ICompanyEngine
    {
        public string[] GetCompanies(Request request)
        {
            var rep = new Repository();

            var companiesRequest2 = new SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("companies", s => s
                        .Field(f => f
                            .CompanyName)));

            var companiesRequest = new SearchRequest
            {
                Size = 0,
                Query = new QueryBuilder().AddDateRangeFilter(request.After, request.Before, "timestamp").Build(),
                Aggregations = new AggregationBuilder()
                        .AddContainer()
                            .AddTermsAggregation("companies", "companyName")
                        .Build()
                    .Build()
            };

            var companiesResult = rep.Search<Log>(companiesRequest);

            var companyList = (from b in companiesResult.Aggs.Terms("companies").Buckets select b.Key).ToArray();

            return companyList;
        }
        public string[] GetCompanyDetails(DetailRequest detailRequest)
        {
            if (detailRequest.CompanyName != null && detailRequest.Username == null)
            {
                return GetUsersofCompany(detailRequest.CompanyName, detailRequest);
            }
            else if (detailRequest.CompanyName != null && detailRequest.Username != null)
            {
                return GetActionbyUserandCompany(detailRequest.CompanyName, detailRequest.Username, detailRequest);
            }
            else
            {
                return new string[0];
            }
        }
        private string[] GetUsersofCompany(string companyName, Request request)
        {
            var rep = new Repository();

            //var usersRequest = new SearchDescriptor<Log>()
            //    .Query(q => q
            //        .MatchPhrase(s => s
            //            .Field(f => f.CompanyName)
            //            .Query(companyName)))
            //    .Size(0)
            //    .Aggregations(aggs => aggs
            //        .Terms("users", s => s
            //            .Field(f => f.Username)));


            var usersRequest = new SearchRequest()
            {
                Query = new QueryBuilder()
                    .AddMatchQuery(companyName, "companyName")
                    .AddDateRangeFilter(request.After, request.Before, "timestamp")
                    .Build(),
                Size = 0,
                Aggregations = new AggregationBuilder()
                    .AddContainer()
                        .AddTermsAggregation("users", "username")
                        .Build()
                    .Build()
            };

            var usersResult = rep.Search<Log>(usersRequest);

            var userList = (from b in usersResult.Aggs.Terms("users").Buckets select b.Key).ToArray();

            return userList;
        }
        private string[] GetActionbyUserandCompany(string companyName, string username, Request request)
        {
            var rep = new Repository();

            var actionsRequest = new SearchDescriptor<Log>()
                .Query(q => q
                    .Bool(b => b
                        .Must(
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f
                                    .CompanyName)
                                .Query(companyName)),
                            mu => mu
                            .MatchPhrase(s => s
                                .Field(f => f
                                    .Username)
                                .Query(username)))))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f
                            .Endpoint)));
            var actionsResult = rep.Search<Log>(actionsRequest);

            var actionsList = (from b in actionsResult.Aggs.Terms("actions").Buckets select b.Key).ToArray();

            return actionsList;
        }
    }
}
