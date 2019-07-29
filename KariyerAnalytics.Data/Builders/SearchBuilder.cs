using Nest;

namespace KariyerAnalytics.Data
{
    public class SearchBuilder<T> where T : class
    {
        private SearchRequest<T> _Request;

        public SearchBuilder()
        {
            _Request = new SearchRequest<T>();
        }

        public SearchBuilder<T> AddQuery(QueryContainer query)
        {
            _Request.Query = query;
            return this;
        }

        public SearchBuilder<T> AddAggregation(AggregationDictionary aggregations)
        {
            _Request.Aggregations = aggregations;
            return this;
        }

        public SearchBuilder<T> SetSize(int size)
        {
            _Request.Size = size;
            return this;
        }

        public SearchRequest<T> Build()
        {
            return _Request;
        }

    }
}
