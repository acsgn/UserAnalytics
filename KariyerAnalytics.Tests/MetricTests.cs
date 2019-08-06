using System;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class MetricTests
    {
        private DateTime _After;
        private DateTime _Before;
        private DateTime _TimeStamp;
        private int _ResponseTime;
        private string _CompanyName;
        private string _Username;
        private string _Endpoint;
        private int? _Size = 10;
        private bool _Ascending = false;

        [SetUp]
        public void Setup()
        {
            _After = DateTime.Now;
            _Before = DateTime.Now;
            _TimeStamp = DateTime.Now;
            _ResponseTime = 100;
            _CompanyName = "";
            _Username = "";
            _Endpoint = "";
        }

        [Test]
        public void GetEndpointMetrics_HappyPath()
        {
            var request = new MetricRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName,
                Username = _Username,
                Size = _Size,
                Ascending = _Ascending
            };

            var entity = new MetricsResponse[]
            {
                new MetricsResponse{
                    Key = _Endpoint,
                    NumberOfRequests = _ResponseTime,
                    MinResponseTime = _ResponseTime,
                    AverageResponseTime = _ResponseTime,
                    MaxResponseTime = _ResponseTime
                }
            };

            var mockRepository = Substitute.For<IMetricRepository>();
            mockRepository.GetEndpointMetrics(_CompanyName, _Username, _Size, _Ascending, _After, _Before).Returns(entity);

            var engine = new MetricEngine(mockRepository);
            var response = engine.GetEndpointMetrics(request);

            Assert.AreEqual(entity[0].Key, response[0].Key);
            Assert.AreEqual(entity[0].NumberOfRequests, response[0].NumberOfRequests);
            Assert.AreEqual(entity[0].MinResponseTime, response[0].MinResponseTime);
            Assert.AreEqual(entity[0].AverageResponseTime, response[0].AverageResponseTime);
            Assert.AreEqual(entity[0].MaxResponseTime, response[0].MaxResponseTime);
        }
    }
}