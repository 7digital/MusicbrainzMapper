using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests
    {
        private const int ValidReleaseId = 12345;
        private TrackDurationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TrackDurationService();
        }

        [Test]
        public async void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            var trackDurations = GetTrackDurations();

            Assert.That(trackDurations, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public async void InvalidReleaseIdReturnsException()
        {
            const int invalidReleaseId = 0;

            _service.Get(invalidReleaseId);
        }

        private IList<int> GetTrackDurations()
        {
            return _service.Get(ValidReleaseId);
        }
    }
}