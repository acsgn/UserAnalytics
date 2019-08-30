using System.Collections.Generic;
using Nest;

namespace UserAnalytics.Data
{
    public class AggregationBuilder<T> where T : class
    {
        private AggregationContainerBuilder<T> _AggregationContainerBuilder;

        public Dictionary<string, AggregationContainer> _AggregationDictionary;

        public AggregationBuilder()
        {
            _AggregationDictionary = new Dictionary<string, AggregationContainer>();
        }

        public AggregationBuilder(AggregationContainerBuilder<T> aggregationContainerBuilder)
        {
            _AggregationContainerBuilder = aggregationContainerBuilder;
            _AggregationDictionary = new Dictionary<string, AggregationContainer>();
        }

        public AggregationContainerBuilder<T> AddContainer()
        {
            return new AggregationContainerBuilder<T>(this);
        }

        public AggregationContainerBuilder<T> FinishSubAggregation()
        {
            _AggregationContainerBuilder._AggregationContainer.Aggregations = new AggregationDictionary(_AggregationDictionary);
            return _AggregationContainerBuilder;
        }

        public AggregationDictionary Build()
        {
            return new AggregationDictionary(_AggregationDictionary);
        }

    }
}
