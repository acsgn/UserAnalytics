using System.Linq;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Client.Entities;
using KariyerAnalytics.Data;

namespace KariyerAnalytics.Business
{
    public class LogEngine
    {
        private readonly static string _IndexName = "logs";

        public void Start()
        {
            var context = new ElasticsearchContext();
            context.CreateIndex<Log>(_IndexName);
        }

        public void Add(LogInformation info)
        {
            var log = new Log()
            {
                CompanyName = info.CompanyName,
                Username = info.Username,
                URL = info.URL,
                Endpoint = info.Endpoint,
                Timestamp = info.Timestamp,
                IP = info.IP,
                ResponseTime = info.ResponseTime
            };

            var rep = new EFRepository();
            rep.Add(_IndexName, log);
        }

        public ResponseMetric GetBestResponseTime(Request request)
        {
            var rep = new EFRepository();
            
            var bestRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f
                            .Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f
                                    .ResponseTime))))
                    .MinBucket("best-response-time", s => s
                        .BucketsPath("actions>average-response-time")));

            
            var bestResult = rep.Search<Log>(bestRequest);
            var bucket = bestResult.Aggs.MaxBucket("best-response-time");

            return new ResponseMetric()
            {
                Actions = bucket.Keys,
                Time = (double)bucket.Value
            };
        }

        public ResponseMetric GetWorstResponseTime(Request request)
        {
            var rep = new EFRepository();

            var worstRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("actions", s => s
                        .Field(f => f.Endpoint)
                        .Aggregations(nestedAggs => nestedAggs
                            .Average("average-response-time", nestedS => nestedS
                                .Field(f => f
                                    .ResponseTime))))
                    .MaxBucket("worst-response-time", s => s
                        .BucketsPath("actions>average-response-time")));
            var worstResult = rep.Search<Log>(worstRequest);
            var bucket = worstResult.Aggs.MaxBucket("worst-response-time");

            return new ResponseMetric(){
                Actions = bucket.Keys,
                Time = (double) bucket.Value
            };
        }

        public string[] GetEndpoints(Request request)
        {
            var rep = new EFRepository();

            var endpointsRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("endpoints", s => s
                        .Field(f => f
                            .Endpoint)));
            var endpointsResult = rep.Search<Log>(endpointsRequest);

            var endpointList = (from b in endpointsResult.Aggs.Terms("endpoints").Buckets select b.Key).ToArray();

            return endpointList;
        }

        public int[] GetResponseTimes(string endpoint, Request request)
        {
            var rep = new EFRepository();

            var responseTimeRequest = new Nest.SearchDescriptor<Log>()
                .Query(q => q
                    .MatchPhrase(s => s
                        .Field(f => f
                            .Endpoint)
                        .Query(endpoint)))
                .Sort(s => s
                    .Ascending(f => f
                        .Timestamp))
                .Fields(f => f
                    .Field(fi => fi
                        .ResponseTime));
            var responseTimeResult = rep.Search<Log>(responseTimeRequest);

            var responseTimeList = (from b in responseTimeResult.Fields select b.ValueOf<Log, int>(p => p.ResponseTime)).ToArray();

            return responseTimeList;
        }

        public string[] GetCompanies(Request request)
        {
            var rep = new EFRepository();

            var companiesRequest = new Nest.SearchDescriptor<Log>()
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("companies", s => s
                        .Field(f => f
                            .CompanyName)));
            var companiesResult = rep.Search<Log>(companiesRequest);

            var companyList = (from b in companiesResult.Aggs.Terms("companies").Buckets select b.Key).ToArray();

            return companyList;
        }

        public string[] GetUsersofCompany(string companyName, Request request)
        {
            var rep = new EFRepository();

            var usersRequest = new Nest.SearchDescriptor<Log>()
                .Query(q => q
                    .MatchPhrase(s => s
                        .Field(f => f
                            .CompanyName)
                        .Query(companyName)))
                .Size(0)
                .Aggregations(aggs => aggs
                    .Terms("users", s => s
                        .Field(f => f
                            .Username)));
            var usersResult = rep.Search<Log>(usersRequest);

            var userList = (from b in usersResult.Aggs.Terms("users").Buckets select b.Key).ToArray();

            return userList;
        }

        public string[] GetActionbyUserandCompany(string companyName, string username, Request request)
        {
            var rep = new EFRepository();

            var actionsRequest = new Nest.SearchDescriptor<Log>()
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
