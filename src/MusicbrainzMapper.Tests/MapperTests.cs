using System;
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
        private ITrackDurationService<int> _trackDurationService;
        private SevenDigitalToMusicBrainzMapper _mapper;
        private ITrackDurationMatcher<Guid> _trackDurationMatcher;
        private readonly IList<int> _trackDurations = new[] { 1, 2, 3 };

        [SetUp]
        public void CreateMapper()
        {
            _trackDurationService = Substitute.For<ITrackDurationService<int>>();
            _trackDurationService.GetAsync(Arg.Any<int>())
                .Returns(Task.FromResult(_trackDurations));

            _trackDurationMatcher = Substitute.For<ITrackDurationMatcher<Guid>>();
            _trackDurationMatcher.FindMatchesAsync(Arg.Any<IList<int>>())
                .Returns(Task.FromResult((IList<Guid>) new List<Guid>()));

            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService, _trackDurationMatcher);
        }

        [Test]
        public async void TrackDurationServiceIsCalledOnce()
        {
            await _mapper.MapAsync(SevenDigitalReleaseId);

            _trackDurationService.Received(1).GetAsync(Arg.Any<int>());
        }

        [Test]
        public async void TrackDurationMatcherIsCalledOnce()
        {
            await _mapper.MapAsync(SevenDigitalReleaseId);

            _trackDurationMatcher.Received(1).FindMatchesAsync(_trackDurations);
        }

        [Test]
        public async void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var result = await _mapper.MapAsync(SevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }

    }
}
