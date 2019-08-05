using Nest;

namespace KariyerAnalytics.Data.Contract
{
    public interface IElasticsearchContext
    {
        ElasticClient GetElasticClient();
    }
}
