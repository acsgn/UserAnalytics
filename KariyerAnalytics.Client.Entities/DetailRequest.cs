﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerAnalytics.Client.Entities
{
    public class DetailRequest : Request
    {
        public string Company { get; set; }
        public string Username { get; set; }
    }
}
