using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var mapper = new SevenDigitalToMusicBrainzMapper();
            const int sevenDigitalReleaseId = 12345;

            var result = mapper.MapAsync(sevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }
    }
}
