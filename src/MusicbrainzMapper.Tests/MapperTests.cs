using NSubstitute;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var trackDurationService = Substitute.For<ITrackDurationService>();

            var mapper = new SevenDigitalToMusicBrainzMapper(trackDurationService);
            const int sevenDigitalReleaseId = 12345;

            var result = mapper.MapAsync(sevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }
    }
}
