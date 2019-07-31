using Nest;

namespace KariyerAnalytics.Data
{
    public class CountBuilder<T> where T : class
    {
        private CountRequest<T> _Request;

        public CountBuilder(string index)
        {
            _Request = new CountRequest<T>(index);
        }

        public CountBuilder<T> AddQuery(QueryContainer query)
        {
            _Request.Query = query;
            return this;
        }
        
        public CountRequest<T> Build()
        {
            return _Request;
        }

    }
}
