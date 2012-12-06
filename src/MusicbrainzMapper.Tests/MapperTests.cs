﻿using System;
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
        private ITrackDurationMatcher _trackDurationMatcher;
        private readonly IList<int> _trackDurations = new[] { 1, 2, 3 };

        [SetUp]
        public void CreateMapper()
        {
            _trackDurationService = Substitute.For<ITrackDurationService>();
            _trackDurationService.Get(Arg.Any<int>())
                .Returns(_trackDurations);

            _trackDurationMatcher = Substitute.For<ITrackDurationMatcher>();
            _trackDurationMatcher.FindMatches(Arg.Any<IList<int>>())
                .Returns(new List<Guid>());

            _mapper = new SevenDigitalToMusicBrainzMapper(_trackDurationService, _trackDurationMatcher);
        }

        [Test]
        public async void TrackDurationServiceIsCalledOnce()
        {
            _mapper.Map(SevenDigitalReleaseId);

            _trackDurationService.Received(1).Get(Arg.Any<int>());
        }

        [Test]
        public async void TrackDurationMatcherIsCalledOnce()
        {
            _mapper.Map(SevenDigitalReleaseId);

            _trackDurationMatcher.Received(1).FindMatches(_trackDurations);
        }

        [Test]
        public async void WhenMappingA7digitalReleaseidIGetAListOfMusicbrainzids()
        {
            var result = _mapper.Map(SevenDigitalReleaseId);
            Assert.That(result, Is.Not.Null);
        }

    }
}
