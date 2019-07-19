using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Service.Entities
{
    public class UserDetailRequest : CompanyDetailRequest
    {
        public string Username { get; set; }
    }
}
