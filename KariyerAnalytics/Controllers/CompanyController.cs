using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KariyerAnalytics.Business;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class CompanyController : ApiController
    {
        [HttpGet]
        public string[] GetCompanies()
        {
            //Request request
            var engine = new LogEngine();
            return engine.GetCompanies(new Request());
        }

        [HttpGet]
        public string[] GetCompanyDetails(DetailRequest detailRequest)
        {
            var engine = new LogEngine();
            return engine.GetCompanyDetails(detailRequest);
        }
    }
}
