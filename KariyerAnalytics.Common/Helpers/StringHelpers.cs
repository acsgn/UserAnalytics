using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace KariyerAnalytics.Common
{
    public static class StringHelpers
    {
        public static string GetQueryJSonFromRequest(IRequest request, ElasticClient elasticClient)
        {
            using (var stream = new MemoryStream())
            {
                elasticClient.Serializer.Serialize(request, stream);
                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
