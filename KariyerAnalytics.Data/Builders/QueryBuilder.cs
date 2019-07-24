using System;
using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Data
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
                Must = _MustQueries,
                Filter = _FilterQueries
            };
        }


        public abstract class TermFilterBase
        {
            readonly string _FieldName;
            readonly string[] _Values;
            readonly string _FilterName;

            public TermFilterBase(string fieldName, string[] values, string filterName = null)

            {
                _FieldName = fieldName;
                _Values = values;
                _FilterName = filterName;
            }

            protected QueryContainer CreateFilterQuery()
            {
                QueryContainer queryContainer = null;

                if (_Values.Length == 1)
                {
                    var termFilter = new TermQuery()
                    {
                        Field = _FieldName,
                        Value = _Values[0]
                    };

                    if (!string.IsNullOrEmpty(_FilterName))
                    {
                        termFilter.Name = _FilterName;
                    }

                    queryContainer = termFilter;
                }
                else if (_Values.Length > 1)
                {
                    List<QueryContainer> should = new List<QueryContainer>();

                    foreach (string value in _Values)
                    {
                        var termFilter = new TermQuery()
                        {
                            Field = _FieldName,
                            Value = value
                        };
                        should.Add(termFilter);
                    }
                    queryContainer = new BoolQuery()
                    {
                        Should = should,
                        Name = _FilterName
                    };
                }
                return queryContainer;
            }


        }
    }
}
