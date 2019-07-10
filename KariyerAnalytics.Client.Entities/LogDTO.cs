using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Client.Entities
{
    public class LogDTO
    {
        public string Company { get; set; }
        public string User { get; set; }
        public string URL { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public int ResponseTime { get; set; }
    }
}
