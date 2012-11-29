using NSubstitute;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MapperTests
    {
        private const int SevenDigitalReleaseId = 12345;
        private ITrackDurationService _trackDurationService;
        private SevenDigitalToMusicBrainzMapper _mapper;

        [SetUp]
        public void CreateMapper()
        {
            _trackDurationService = Substitute.For<ITrackDurationService>();

            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService);
        }

        [Test]
        public void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var result = _mapper.MapAsync(SevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }

    }
}
