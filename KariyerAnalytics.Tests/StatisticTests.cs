using System;
using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class StatisticTests
    {
        private DateTime _After;
        private DateTime _Before;
        private DateTime _TimeStamp;
        private string _Endpoint;
        private TimeSpan _Interval;
        private int _ResponseTime;


        [SetUp]
        public void Setup()
        {
             _After = DateTime.Now;
             _Before = DateTime.Now;
             _TimeStamp = DateTime.Now;
             _Endpoint = "";
             _Interval = new TimeSpan();
             _ResponseTime = 100;
        }

        [Test]
        public void GetBestResponseTime_HappyPath()
        {
            var request = new Request()
            {
                After = _After,
                Before = _Before
            };

            var entity = new MetricResponse
                {
                    Endpoint = _Endpoint,
                    AverageResponseTime = _ResponseTime
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetBestResponseTime(_After, _Before).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetBestResponseTime(request);

            Assert.AreEqual(entity.AverageResponseTime, response.AverageResponseTime);
            Assert.AreEqual(entity.Endpoint, response.Endpoint);
        }

        [Test]
        public void GetWorstResponseTime_HappyPath()
        {
            var request = new Request()
            {
                After = _After,
                Before = _Before
            };

            var entity = new MetricResponse
            {
                Endpoint = _Endpoint,
                AverageResponseTime = _ResponseTime
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetWorstResponseTime(_After, _Before).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetWorstResponseTime(request);

            Assert.AreEqual(entity.AverageResponseTime, response.AverageResponseTime);
            Assert.AreEqual(entity.Endpoint, response.Endpoint);
        }

        [Test]
        public void GetRealtimeUsers_HappyPath()
        {
            var request = new RealtimeUserCountRequest()
            {
                SecondsBefore = _ResponseTime
            };

            var entity = new RealtimeUserMetric[] {
                new RealtimeUserMetric{
                    Endpoint = _Endpoint,
                    UserCount = _ResponseTime
                }
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetRealtimeUsers(_ResponseTime).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetRealtimeUsers(request);

            Assert.AreEqual(entity[0].UserCount, response[0].UserCount);
            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
        }

        [Test]
        public void GetEndpoints_HappyPath()
        {
            var request = new Request
            {
                After = _After,
                Before = _Before
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetEndpoints(_After, _Before).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetEndpoints(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetResponseTimes_HappyPath()
        {
            var request = new ResponseTimeRequest()
            {
                After = _After,
                Before = _Before,
                Endpoint = _Endpoint,
                Interval = _Interval
            };

            var entity = new Histogram[]
                {
                    new Histogram
                    {
                        Average = _ResponseTime,
                        Timestamp = _TimeStamp
                    }
                };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetResponseTimesByEndpoint(_Endpoint, _Interval, _After, _Before).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetResponseTimes(request);

            Assert.AreEqual(entity[0].Average, response[0].Average);
            Assert.AreEqual(entity[0].Timestamp, response[0].Timestamp);
        }

    }
}