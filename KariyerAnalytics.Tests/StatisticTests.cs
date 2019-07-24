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
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public void Test1()
        //{
        //    Mock<Request> mockRequest = new Mock<Request>();
        //    Mock<StatisticRepository> mockStatisticRepository = new Mock<StatisticRepository>();
        //    StatisticEngine statisticEngine = new StatisticEngine(mockStatisticRepository.Object);

        //    var response = statisticEngine.GetBestResponseTime(mockRequest.Object);

        //    Assert.That(response, Is.Not.Null);
        //}


        [Test]
        public void GetResponseTimes_HappyPath()
        {
            var after = new DateTime(2019, 7, 23, 15, 0, 0);
            var before = new DateTime(2019, 7, 23, 15, 47, 0);
            var timeStamp = new DateTime(2019, 7, 23, 00, 00, 0);
            var endpoint = "";
            var interval = new TimeSpan(1000);

            var entity = new Histogram[]
                {
                    new Histogram()
                    {
                        Average = 1000,
                        Timestamp = timeStamp

                    }
                };

            var mockRepository = Substitute.For<IStatisticRepository>();
            mockRepository.GetResponseTimes(endpoint, interval, after, before).Returns(entity);
            var engine = new StatisticEngine(mockRepository);
            var request = new ResponseTimeRequest()
            {
                After = after,
                Before = before,
                Endpoint = endpoint,
                Interval = interval
            };

            var response = engine.GetResponseTimes(request);

            Assert.AreEqual(entity[0].Average, response[0].Average);
        }

    }
}