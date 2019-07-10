using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Client.Entities
{
    public class LogInformation
    {
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string URL { get; set; }
        public string Endpoint { get; set; }
        public int ResponseTime { get; set; }
    }
}
