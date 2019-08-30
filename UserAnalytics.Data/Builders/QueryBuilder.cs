using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest;

namespace UserAnalytics.Data
{
    public class QueryBuilder<T> where T : class
    {
        private List<QueryContainer> _FilterQueries;

        public QueryBuilder()
        {
            _FilterQueries = new List<QueryContainer>();
        }

        public QueryBuilder<T> AddMatchQuery(string term, Expression<Func<T, object>> field)
        {
            _FilterQueries.Add(new MatchQuery()
            {
                Field = field,
                Query = term
            });
            return this;
        }

        public QueryBuilder<T> AddMatchPhraseQuery(string term, Expression<Func<T, object>> field)
        {
            _FilterQueries.Add(new MatchPhraseQuery()
            {
                Field = field,
                Query = term
            });
            return this;
        }

        public QueryBuilder<T> AddDateRangeQuery(DateTime? gte, DateTime? lte, Expression<Func<T, object>> field)
        {
            _FilterQueries.Add(new DateRangeQuery()
            {
                Field = field,
                GreaterThanOrEqualTo = gte,
                LessThanOrEqualTo = lte
            });

            return this;
        }

        public QueryBuilder<T> AddPrefixMatchQuery(string term, Expression<Func<T, object>> field)
        {
            _FilterQueries.Add(new PrefixQuery()
            {
                Field = field,
                Value = term
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
