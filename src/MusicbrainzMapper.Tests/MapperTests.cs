using System;
using System.Collections.Generic;
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
            _trackDurationService.Get(Arg.Any<int>())
                .Returns(_trackDurations);

            _trackDurationMatcher = Substitute.For<ITrackDurationMatcher<Guid>>();
            _trackDurationMatcher.FindMatches(Arg.Any<IList<int>>())
                .Returns(new List<Guid>());

            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService, _trackDurationMatcher);
        }

        [Test]
        public void TrackDurationServiceIsCalledOnce()
        {
            _mapper.Map(SevenDigitalReleaseId);

            _trackDurationService.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public void TrackDurationMatcherIsCalledOnce()
        {
            _mapper.Map(SevenDigitalReleaseId);

            _trackDurationMatcher.Received(1).FindMatches(_trackDurations);
        }

        [Test]
        public void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var result = _mapper.Map(SevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }

    }
}
