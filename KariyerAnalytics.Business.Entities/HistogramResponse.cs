using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Business.Entities
{
    public class HistogramResponse
    {
        public DateTime Timestamp { get; set; }
        public long NumberOfRequests { get; set; }
        public double Average { get; set; }
    }
}
