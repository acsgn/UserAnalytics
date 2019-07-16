using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data
{
    class AggregationBuilder
    {
        internal string IndexName;
        internal int Size;
        internal int From;
        internal IAggregationContainer AggregationContainer;

        public AggregationBuilder(string indexName)
        {
            IndexName = indexName;
            AggregationContainer = new AggregationContainer();
        }

        public AggregationBuilder SetSize(int size)
        {
            Size = size;

            return this;
        }

        public AggregationBuilder SetFrom(int from)
        {
            From = from;

            return this;
        }

        public AggregationBuilder AddAggregationQuery()
        {
            return this;
        }
        
    }
}
