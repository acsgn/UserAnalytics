﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface ICompanyEngine
    {
        string[] GetCompanies(Request request);

        string[] GetCompanyDetails(DetailRequest detailRequest);
       
    }
}
