using System;

namespace KariyerAnalytics.Service.Entities
{
    public class UserDetailRequest
    {
        public DateTime After { get; set; }
        public DateTime Before { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
    }
}
