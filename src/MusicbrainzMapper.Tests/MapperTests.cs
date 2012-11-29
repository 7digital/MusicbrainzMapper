using System.Collections.Generic;
using System.Threading.Tasks;
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
            _trackDurationService.GetAsync(Arg.Any<int>())
                .Returns(Task.FromResult((IList<int>) new List<int>()));

            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService);
        }

        [Test, Ignore("async mocking is hard")]
        public async void TrackDurationServiceIsCalledOnce()
        {
            await _mapper.MapAsync(SevenDigitalReleaseId);

            await _trackDurationService.Received().GetAsync(Arg.Any<int>());
        }

        [Test]
        public async void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var result = await _mapper.MapAsync(SevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }

    }
}
