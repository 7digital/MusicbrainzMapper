using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests
    {
        [Test]
        public void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            const int releaseId = 12345;
            var service = new TrackDurationService();

            Assert.That(service.GetAsync(releaseId), Is.Not.Null);
        }
    }
}