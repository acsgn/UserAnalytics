using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Data.Entities
{
    public class SearchResponse<T> where T : class
    {
        public IEnumerable<T> Documents { get; protected set; }
        public AggregationsHelper AggregationsHelper { get; protected set; }
    }
}
