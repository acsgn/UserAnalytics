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
        private int _ResponseTime;
        private string _CompanyName;
        private string _Username;
        private string _Endpoint;


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
        public void GetBestResponseTime_HappyPath()
        {
            var request = new Request()
            {
                After = _After,
                Before = _Before
            };

            var entity = new EndpointAbsoluteMetricsResponse
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

            var entity = new EndpointAbsoluteMetricsResponse
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
        public void GetEndpointMetrics_HappyPath()
        {
            var request = new StatisticRequest
            {
                After = _After,
                Before = _Before
            };

            var entity = new EndpointMetricsResponse[]
            {
                new EndpointMetricsResponse{
                    Endpoint = _Endpoint,
                    NumberOfRequests = _ResponseTime,
                    MinResponseTime = _ResponseTime,
                    AverageResponseTime = _ResponseTime,
                    MaxResponseTime = _ResponseTime
                }
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetEndpointMetrics(_After, _Before).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetEndpointMetricsbyCompanyAndUser(request);

            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
            Assert.AreEqual(entity[0].NumberOfRequests, response[0].NumberOfRequests);
            Assert.AreEqual(entity[0].MinResponseTime, response[0].MinResponseTime);
            Assert.AreEqual(entity[0].AverageResponseTime, response[0].AverageResponseTime);
            Assert.AreEqual(entity[0].MaxResponseTime, response[0].MaxResponseTime);
        }

        [Test]
        public void GetEndpointMetricsbyCompany_HappyPath()
        {
            var request = new StatisticRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName
            };

            var entity = new EndpointMetricsResponse[]
            {
                new EndpointMetricsResponse{
                    Endpoint = _Endpoint,
                    NumberOfRequests = _ResponseTime,
                    MinResponseTime = _ResponseTime,
                    AverageResponseTime = _ResponseTime,
                    MaxResponseTime = _ResponseTime
                }
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetEndpointMetrics(_After, _Before, _CompanyName).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetEndpointMetricsbyCompanyAndUser(request);

            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
            Assert.AreEqual(entity[0].NumberOfRequests, response[0].NumberOfRequests);
            Assert.AreEqual(entity[0].MinResponseTime, response[0].MinResponseTime);
            Assert.AreEqual(entity[0].AverageResponseTime, response[0].AverageResponseTime);
            Assert.AreEqual(entity[0].MaxResponseTime, response[0].MaxResponseTime);
        }

        [Test]
        public void GetEndpointMetricsbyCompanyAndUser_HappyPath()
        {
            var request = new StatisticRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName,
                Username = _Username
            };

            var entity = new EndpointMetricsResponse[]
            {
                new EndpointMetricsResponse{
                    Endpoint = _Endpoint,
                    NumberOfRequests = _ResponseTime,
                    MinResponseTime = _ResponseTime,
                    AverageResponseTime = _ResponseTime,
                    MaxResponseTime = _ResponseTime
                }
            };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetEndpointMetrics(_After, _Before, _CompanyName, _Username).Returns(entity);

            var engine = new StatisticEngine(mockRepository);
            var response = engine.GetEndpointMetricsbyCompanyAndUser(request);

            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
            Assert.AreEqual(entity[0].NumberOfRequests, response[0].NumberOfRequests);
            Assert.AreEqual(entity[0].MinResponseTime, response[0].MinResponseTime);
            Assert.AreEqual(entity[0].AverageResponseTime, response[0].AverageResponseTime);
            Assert.AreEqual(entity[0].MaxResponseTime, response[0].MaxResponseTime);
        }
    }
}