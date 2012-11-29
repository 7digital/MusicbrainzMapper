using NUnit.Framework;

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
    }
}