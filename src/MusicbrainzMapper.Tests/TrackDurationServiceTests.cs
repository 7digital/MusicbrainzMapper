using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests
    {
        [Test]
        public async void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            const int releaseId = 12345;
            var service = new TrackDurationService();

            var trackDurations = await service.GetAsync(releaseId);

            Assert.That(trackDurations, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public async void InvalidReleaseIdReturnsException()
        {
            const int invalidReleaseId = 0;
            var service = new TrackDurationService();

            var trackDurations = await service.GetAsync(invalidReleaseId);
        }
    }
}