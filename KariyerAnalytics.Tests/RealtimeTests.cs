using KariyerAnalytics.Business;
using KariyerAnalytics.Business.Entities;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class RealtimeTests
    {
        private int _SecondsBefore;
        private string _Endpoint;


        [SetUp]
        public void Setup()
        {
            _SecondsBefore = 5;
            _Endpoint = "";
        }

        [Test]
        public void GetRealtimeUserCount_HappyPath()
        {
            var request = new RealtimeRequest()
            {
                SecondsBefore = _SecondsBefore
            };

            var mockRepository = Substitute.For<IRealtimeRepository>();
            mockRepository.GetRealtimeUserCount(_SecondsBefore).Returns(_SecondsBefore);

            var engine = new RealtimeEngine(mockRepository);
            var response = engine.GetRealtimeUserCount(request);

            Assert.AreEqual(response, _SecondsBefore);
        }

        [Test]
        public void GetRealtimeUserCountByEndpoint_HappyPath()
        {
            var request = new RealtimeRequest()
            {
                SecondsBefore = _SecondsBefore
            };

            var entity = new RealtimeUserCountResponse[] {
                new RealtimeUserCountResponse{
                    Endpoint = _Endpoint,
                    UserCount = _SecondsBefore
                }
            };

            var mockRepository = Substitute.For<IRealtimeRepository>();
            mockRepository.GetRealtimeUserCountByEndpoints(_SecondsBefore).Returns(entity);

            var engine = new RealtimeEngine(mockRepository);
            var response = engine.GetRealtimeUserCountByEndpoints(request);

            Assert.AreEqual(entity[0].UserCount, response[0].UserCount);
            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
        }
    }
}
