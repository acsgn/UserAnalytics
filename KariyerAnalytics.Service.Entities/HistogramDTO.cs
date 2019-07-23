using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Service.Entities
{
    public class HistogramDTO
    {
        public DateTime Timestamp { get; set; }
        public double Average { get; set; }
    }
}
