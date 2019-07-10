using System.Collections.Generic;
using System.Threading.Tasks;

namespace KariyerAnalytics.Data
{
    public interface IRepository
    {
        Task<IEnumerable<T>> Search<T>() where T : class;
        void Add<T>(string indexname, T entity) where T : class;
    }
}
