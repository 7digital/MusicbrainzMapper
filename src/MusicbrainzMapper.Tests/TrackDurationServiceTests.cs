﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MusicbrainzMapper.Tests.AsyncSanityTests;
using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests : AsyncSanityTest
    {
        private const int ValidReleaseId = 12345;
        private SevenDigitalTrackDurationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new SevenDigitalTrackDurationService();
        }

        [Test]
        public async void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            var trackDurations = await GetTrackDurations();

            Assert.That(trackDurations, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public async void InvalidReleaseIdReturnsException()
        {
            const int invalidReleaseId = 0;

            await _service.GetAsync(invalidReleaseId);
        }

        protected async override Task ToTest()
        {
            await GetTrackDurations();
        }

        private async Task<IList<int>> GetTrackDurations()
        {
            return await _service.GetAsync(ValidReleaseId);
        }
    }
}