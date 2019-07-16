using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Business.Entities
{
    public class ResponseMetric
    {
        public IList<string> Actions { get; set; }
        public double Time { get; set; }
    }
}
