﻿using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface ILogRepository
    {
        void CreateIndex();
        void Index(Log log);
    }
}