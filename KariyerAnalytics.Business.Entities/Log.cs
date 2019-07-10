using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KariyerAnalytics.Business.Entities
{
    public class Log
    {
        public string Company { get; set; }
        public string User { get; set; }
        public string URL { get; set; }
        public string Endpoint { get; set; }
        public double Date { get; set; }
        public string IP { get; set; }
        public int ResponseTime { get; set; }
    }
}