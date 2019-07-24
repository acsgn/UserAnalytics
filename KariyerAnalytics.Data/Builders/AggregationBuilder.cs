using System.Collections.Generic;
using Nest;

namespace KariyerAnalytics.Data
{
    public class AggregationBuilder
    {
        private AggregationContainerBuilder _AggregationContainerBuilder;

        public Dictionary<string, AggregationContainer> _AggregationDictionary;

        public AggregationBuilder()
        {
            _AggregationDictionary = new Dictionary<string, AggregationContainer>();
        }

        public AggregationBuilder(AggregationContainerBuilder aggregationContainerBuilder)
        {
            _AggregationContainerBuilder = aggregationContainerBuilder;
            _AggregationDictionary = new Dictionary<string, AggregationContainer>();
        }

        public AggregationContainerBuilder AddContainer()
        {
            return new AggregationContainerBuilder(this);
        }

        public AggregationContainerBuilder FinishSubAggregation()
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
