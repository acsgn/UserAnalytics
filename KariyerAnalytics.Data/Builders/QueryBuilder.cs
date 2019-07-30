using System;
using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Data
{
    public class QueryBuilder
    {
        private List<QueryContainer> _FilterQueries;

        public QueryBuilder()
        {
            _FilterQueries = new List<QueryContainer>();
        }

        public QueryBuilder AddMatchQuery(string term, string field)
        {
            _FilterQueries.Add(new MatchQuery()
            {
                Field = new Field()
                {
                    Name = field
                },
                Query = term
            });
            return this;
        }

        public QueryBuilder AddMatchPhraseQuery(string term, string field)
        {
            _FilterQueries.Add(new MatchPhraseQuery()
            {
                Field = new Field()
                {
                    Name = field
                },
                Query = term
            });
            return this;
        }

        public QueryBuilder AddDateRangeQuery(DateTime? gte, DateTime? lte, string field)
        {
            _FilterQueries.Add(new DateRangeQuery()
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
                Filter = _FilterQueries
            };
        }
    }
}
