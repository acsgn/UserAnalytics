﻿using System;
using UserAnalytics.Business;
using UserAnalytics.Business.Entities;
using UserAnalytics.Data.Contract;
using UserAnalytics.Service.Entities;
using NSubstitute;
using NUnit.Framework;

namespace UserAnalytics.Tests
{
    public class HistogramTests
    {
        private DateTime _After;
        private DateTime _Before;
        private DateTime _Timestamp;
        private TimeSpan _Interval;
        private string _Endpoint;
        private double _Average;
        private long _NumberOfRequests;


        [SetUp]
        public void Setup()
        {
            _After = DateTime.Now;
            _Before = DateTime.Now;
            _Timestamp = DateTime.Now;
            _Interval = new TimeSpan();
            _Endpoint = "";
            _Average = 100;
            _NumberOfRequests = 100;
        }

        [Test]
        public void GetResponseTimesHistogram_HappyPath()
        {
            var request = new HistogramRequest
            {
                After = _After,
                Before = _Before,
                Interval = _Interval,
                Endpoint = _Endpoint
            };

            var entity = new HistogramResponse[] {
                new HistogramResponse
                {
                    Average = _Average,
                    NumberOfRequests = _NumberOfRequests,
                    Timestamp = _Timestamp
                }
            };

            var mockRepository = Substitute.For<IHistogramRepository>();
            mockRepository.GetResponseTimesHistogram(_Endpoint, _Interval, _After, _Before).Returns(entity);

            var engine = new HistogramEngine(mockRepository);
            var response = engine.GetResponseTimesHistogram(request);

            Assert.AreEqual(entity[0].Average, response[0].Average);
            Assert.AreEqual(entity[0].Timestamp, response[0].Timestamp);
            Assert.AreEqual(entity[0].NumberOfRequests, response[0].NumberOfRequests);
        }
    }
}
