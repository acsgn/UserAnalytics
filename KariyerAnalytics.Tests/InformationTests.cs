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


        [SetUp]
        public void Setup()
        {
            _After = DateTime.Now;
            _Before = DateTime.Now;
            _CompanyName = "";
            _Username = "";
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
            mockRepository.GetCompanyUsers(_CompanyName, _After, _Before).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetCompanyUsers(request);

            Assert.AreEqual(entity[0], response[0]);
        }


        [Test]
        public void GetEndpoints_HappyPath()
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
            mockRepository.GetEndpoints(_After, _Before).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetEndpoints(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetEndpointsByCompany_HappyPath()
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
            mockRepository.GetEndpoints(_After, _Before, _CompanyName).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetEndpointsByCompany(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetEndpointsByCompanyAndUser_HappyPath()
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
            mockRepository.GetEndpoints(_After, _Before, _CompanyName, _Username).Returns(entity);

            var engine = new InformationEngine(mockRepository);
            var response = engine.GetEndpointsByCompanyAndUser(request);

            Assert.AreEqual(entity[0], response[0]);
        }
    }
}
