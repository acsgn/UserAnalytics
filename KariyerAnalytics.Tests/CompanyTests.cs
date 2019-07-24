using System;
using KariyerAnalytics.Business;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace KariyerAnalytics.Tests
{
    public class CompanyTests
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
            var request = new Request
            {
                After = _After,
                Before = _Before
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<ICompanyRepository>();
            mockRepository.GetCompanies(_After, _Before).Returns(entity);

            var engine = new CompanyEngine(mockRepository);
            var response = engine.GetCompanies(request);

            Assert.AreEqual(entity[0], response[0]);
        }

        [Test]
        public void GetCompanyUsers_HappyPath()
        {
            var request = new CompanyDetailRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<ICompanyRepository>();
            mockRepository.GetCompanyUsers(_CompanyName, _After, _Before).Returns(entity);

            var engine = new CompanyEngine(mockRepository);
            var response = engine.GetCompanyUsers(request);

            Assert.AreEqual(entity[0], response[0]);
        }


        [Test]
        public void GetEndpointsbyUserandCompany_HappyPath()
        {
            var request = new UserDetailRequest
            {
                After = _After,
                Before = _Before,
                CompanyName = _CompanyName,
                Username = _Username
            };

            var entity = new string[] {
                ""
            };

            var mockRepository = Substitute.For<ICompanyRepository>();
            mockRepository.GetEndpointsbyUserandCompany(_CompanyName, _Username,_After, _Before).Returns(entity);

            var engine = new CompanyEngine(mockRepository);
            var response = engine.GetEndpointsbyUserandCompany(request);

            Assert.AreEqual(entity[0], response[0]);
        }

    }
}
