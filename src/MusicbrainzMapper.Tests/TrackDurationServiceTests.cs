using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests : AsyncSanityTest
    {
        private TrackDurationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TrackDurationService();
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

            var trackDurations = await _service.GetAsync(invalidReleaseId);
        }

        protected async override Task ToTest()
        {
            await GetTrackDurations();
        }

        private async Task<IList<int>> GetTrackDurations()
        {
            const int releaseId = 12345;

            return await _service.GetAsync(releaseId);
        }
    }
}