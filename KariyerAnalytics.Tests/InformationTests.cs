using KariyerAnalytics.Business;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class InformationTests
    {
        private string _CompanyName;
        private string _Username;
        private string _Endpoint;
        private int? _Size;


        [SetUp]
        public void Setup()
        {
            _CompanyName = "";
            _Username = "";
            _Endpoint = "";
            _Size = 5;
        }

        [Test]
        public void GetCompanies_HappyPath()
        {
            var request = new InformationRequest
            {
                CompanyName = _CompanyName,
                Username = _Username,
                Endpoint = _Endpoint,
                Size = _Size
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetCompanies(_Endpoint, _CompanyName, _Username, _Size).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetCompanies(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetUsers_HappyPath()
        {
            var request = new InformationRequest
            {
                CompanyName = _CompanyName,
                Username = _Username,
                Endpoint = _Endpoint,
                Size = _Size
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetUsers(_Endpoint, _CompanyName, _Username, _Size).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetUsers(request);

            Assert.AreEqual(entity[0], response[0]);
        }


        [Test]
        public void GetEndpoints_HappyPath()
        {
            var request = new InformationRequest
            {
                CompanyName = _CompanyName,
                Username = _Username,
                Endpoint = _Endpoint,
                Size = _Size
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetEndpoints(_Endpoint, _CompanyName, _Username, _Size).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetEndpoints(request);

            Assert.AreEqual(entity[0], response[0]);
        }
    }
}
