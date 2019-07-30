using System;
using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Data
{
    public class AggregationContainerBuilder
    {
        private AggregationBuilder _AggregationBuilder;

        public AggregationContainer _AggregationContainer;

        private string _Name;

        public AggregationContainerBuilder(AggregationBuilder aggregationBuilder)
        {
            _AggregationBuilder = aggregationBuilder;
        }

        public AggregationContainerBuilder AddFilterAggregation(string name, QueryContainer query)
        {
            _Name = name;
            _AggregationContainer = new FilterAggregation(name)
            {
                Filter = query
            };
            return this;
        }

        public AggregationContainerBuilder AddTermsAggregation(string name, string field, int? size, string orderKey, bool? ascending)
        {
            _Name = name;
            _AggregationContainer = new TermsAggregation(name)
            {
                Field = field,
                Size = size,
                Order = string.IsNullOrEmpty(orderKey) && !ascending.HasValue ? null : new List<TermsOrder>
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

        public AggregationContainerBuilder AddDateHistogram(string name, string field, TimeSpan interval)
        {
            _Name = name;
            _AggregationContainer = new DateHistogramAggregation(name)
            {
                Field = field,
                Interval = new Union<DateInterval, Time>(interval)
            };
            return this;
        }

        public AggregationContainerBuilder AddAverageAggregation(string name, string field)
        {
            _Name = name;
            _AggregationContainer = new AverageAggregation(name, field);
            return this;
        }

        public AggregationContainerBuilder AddMaxAggregation(string name, string field)
        {
            _Name = name;
            _AggregationContainer = new MaxAggregation(name, field);
            return this;
        }

        public AggregationContainerBuilder AddMinAggregation(string name, string field)
        {
            _Name = name;
            _AggregationContainer = new MinAggregation(name, field);
            return this;
        }

        public AggregationBuilder AddSubAggregation()
        {
            return new AggregationBuilder(this);
        }

        public AggregationBuilder Build()
        {
            _AggregationBuilder._AggregationDictionary.Add(_Name, _AggregationContainer);
            return _AggregationBuilder;
        }
    }
}
