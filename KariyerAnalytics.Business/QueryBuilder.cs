using System;
using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Business
{
    public class QueryBuilder
    {
        private List<QueryContainer> _MustQueries;
        private List<QueryContainer> _FilterQueries;

        public QueryBuilder()
        {
            _FilterQueries = new List<QueryContainer>();
            _MustQueries = new List<QueryContainer>();
        }

        public QueryBuilder AddMatchQuery(string term, string field)
        {
            _MustQueries.Add(new MatchQuery()
            {
                Field = new Field()
                {
                    Name = field
                },
                Query = term
            });
            return this;
        }

        public QueryBuilder AddDateRangeFilter(DateTime gte, DateTime lte, string field)
        {
            _FilterQueries.Add( new DateRangeQuery()
            {
                Field = new Field
                {
                    Name = field
                },
                GreaterThanOrEqualTo = gte,
                LessThanOrEqualTo = lte
            });

            return this;
        }

        public QueryContainer Build()
        {
            return new BoolQuery
            {
                Must = _MustQueries,
                Filter = _FilterQueries
            };
        }

    }
}
