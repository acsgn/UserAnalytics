using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Business
{
    public class AggregationBuilder
    {

        private Dictionary<string, IAggregationContainer> _Aggregations;

        public AggregationBuilder Add()
        {
            var x = new TermsAggregation("xyz")
                {
                    Field = "xyz"
                
            });
            _Aggregations.Add("asd", x);
            return this;
        }

        public AggregationDictionary Build()
        {
            return new AggregationDictionary(_Aggregations);
        }

    }
}
