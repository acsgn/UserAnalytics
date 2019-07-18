using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Business
{
    public class AggregationContainerBuilder
    {
        private AggregationBuilder _AggregationBuilder;

        public AggregationContainer _AggregationContainer;

        private string _Name;
        private bool _CanBeSubAggregated = false;

        public AggregationContainerBuilder(AggregationBuilder aggregationBuilder)
        {
            _AggregationBuilder = aggregationBuilder;
        }

        public AggregationContainerBuilder AddTermsAggregation(string name, string field)
        {
            _Name = name;
            _CanBeSubAggregated = true;
            _AggregationContainer = new TermsAggregation(name)
            {
                Field = field
            };
            return this;
        }

        public AggregationContainerBuilder AddAverageAggregation(string name, string field)
        {
            _Name = name;
            _CanBeSubAggregated = true;
            _AggregationContainer = new AverageAggregation(name, field);
            return this;
        }

        public AggregationContainerBuilder AddMaxBucketAggregation(string name, string bucketsPath)
        {
            _Name = name;
            _AggregationContainer = new MaxBucketAggregation(name, new SingleBucketsPath(bucketsPath));
            return this;
        }

        public AggregationContainerBuilder AddMinBucketAggregation(string name, string bucketsPath)
        {
            _Name = name;
            _AggregationContainer = new MinBucketAggregation(name, new SingleBucketsPath(bucketsPath));
            return this;
        }

        public AggregationBuilder AddSubAggregation()
        {
            if (_CanBeSubAggregated)
            {
                return new AggregationBuilder(this);
            }
            else
            {
                throw new Exception("Can't sub aggregate");
            }
        }

        public AggregationBuilder Build()
        {
            _AggregationBuilder._AggregationDictionary.Add(_Name, _AggregationContainer);
            return _AggregationBuilder;
        }
    }
}
