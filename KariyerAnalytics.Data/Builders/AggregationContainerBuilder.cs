using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest;

namespace KariyerAnalytics.Data
{
    public class AggregationContainerBuilder<T> where T : class
    {
        private AggregationBuilder<T> _AggregationBuilder;

        public AggregationContainer _AggregationContainer;

        private string _Name;

        public AggregationContainerBuilder(AggregationBuilder<T> aggregationBuilder)
        {
            _AggregationBuilder = aggregationBuilder;
        }

        public AggregationContainerBuilder<T> AddFilterAggregation(string name, QueryContainer query)
        {
            _Name = name;
            _AggregationContainer = new FilterAggregation(name)
            {
                Filter = query
            };
            return this;
        }

        public AggregationContainerBuilder<T> AddTermsAggregation(string name, Expression<Func<T, object>> field, int? size, string orderKey = null, bool? ascending = null)
        {
            _Name = name;
            _AggregationContainer = new TermsAggregation(name)
            {
                Field = field,
                Size = size,
                Order = string.IsNullOrEmpty(orderKey) || !ascending.HasValue ? null : new List<TermsOrder>
                {
                    new TermsOrder()
                    {
                        Key = orderKey,
                        Order = ascending.Value ? SortOrder.Ascending : SortOrder.Descending
                    }
                }
            };
            return this;
        }

        public AggregationContainerBuilder<T> AddDateHistogram(string name, Expression<Func<T, object>> field, TimeSpan interval)
        {
            _Name = name;
            _AggregationContainer = new DateHistogramAggregation(name)
            {
                Field = field,
                Interval = new Union<DateInterval, Time>(interval)
            };
            return this;
        }

        public AggregationContainerBuilder<T> AddAverageAggregation(string name, Expression<Func<T, object>> field)
        {
            _Name = name;
            _AggregationContainer = new AverageAggregation(name, field);
            return this;
        }

        public AggregationContainerBuilder<T> AddMaxAggregation(string name, Expression<Func<T, object>> field)
        {
            _Name = name;
            _AggregationContainer = new MaxAggregation(name, field);
            return this;
        }

        public AggregationContainerBuilder<T> AddMinAggregation(string name, Expression<Func<T, object>> field)
        {
            _Name = name;
            _AggregationContainer = new MinAggregation(name, field);
            return this;
        }

        public AggregationBuilder<T> AddSubAggregation()
        {
            return new AggregationBuilder<T>(this);
        }

        public AggregationBuilder<T> Build()
        {
            _AggregationBuilder._AggregationDictionary.Add(_Name, _AggregationContainer);
            return _AggregationBuilder;
        }
    }
}
