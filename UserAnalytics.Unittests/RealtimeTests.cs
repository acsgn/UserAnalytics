using UserAnalytics.Business;
using UserAnalytics.Business.Entities;
using UserAnalytics.Data.Contract;
using UserAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace UserAnalytics.Tests
{
    public class RealtimeTests
    {
        private int _SecondsBefore;
        private int? _Size;
        private string _Endpoint;


        [SetUp]
        public void Setup()
        {
            _SecondsBefore = 5;
            _Endpoint = "";
            _Size = 10;
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
                SecondsBefore = _SecondsBefore,
                Size = _Size
            };

            var entity = new RealtimeUserCountResponse[] {
                new RealtimeUserCountResponse{
                    Endpoint = _Endpoint,
                    UserCount = _SecondsBefore
                }
            };

            var mockRepository = Substitute.For<IRealtimeRepository>();
            mockRepository.GetEndpointsRealtimeUserCount(_SecondsBefore, _Size).Returns(entity);

            var engine = new RealtimeEngine(mockRepository);
            var response = engine.GetEndpointsRealtimeUserCount(request);

            Assert.AreEqual(entity[0].UserCount, response[0].UserCount);
            Assert.AreEqual(entity[0].Endpoint, response[0].Endpoint);
        }
    }
}
