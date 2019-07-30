using System;
using KariyerAnalytics.Business;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class InformationTests
    {
        private DateTime _After;
        private DateTime _Before;
        private string _CompanyName;
        private string _Username;
        private string _Endpoint;


        [SetUp]
        public void Setup()
        {
            _After = DateTime.Now;
            _Before = DateTime.Now;
            _CompanyName = "";
            _Username = "";
            _Endpoint = "";
        }

        [Test]
        public void GetCompanies_HappyPath()
        {
            var request = new InformationRequest
            {
                After = _After,
                Before = _Before
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetCompanies(_After, _Before).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetCompanies(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetCompanyUsers_HappyPath()
        {
            var request = new InformationRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetUsers(_Endpoint, _CompanyName, _After, _Before).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetUsers(request);

            Assert.AreEqual(entity[0], response[0]);
        }


        [Test]
        public void GetEndpoints_HappyPath()
        {
            var request = new InformationRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName,
                Username = _Username
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<IInformationRepository>();
            mockRepository.GetEndpoints(_CompanyName, _Username, _After, _Before).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetEndpoints(request);

            Assert.AreEqual(entity[0], response[0]);
        }
    }
}
