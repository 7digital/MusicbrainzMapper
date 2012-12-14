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
        private SevenDigitalTrackDurationService _service;

        [SetUp]
        public void Setup()
        {
            _service = new SevenDigitalTrackDurationService();
        }

        [Test]
        public void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            var trackDurations = GetTrackDurations();

            Assert.That(trackDurations, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public void InvalidReleaseIdReturnsException()
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