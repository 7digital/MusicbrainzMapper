﻿using System;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MusicbrainzMapperIntegrationTests
    {
        private SevenDigitalToMusicBrainzMapper _mapper;
        private const string ConnectionString = "Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;";

        [TestFixtureSetUp]
        public void SetUp()
        {
            var matcher = new TrackDurationMatcher(ConnectionString);
            var durationService = new TrackDurationService();
            _mapper = new SevenDigitalToMusicBrainzMapper(durationService, matcher);
        }

        [TestCase(287887, "e956c901-acb7-48d6-9dc6-389a5f91f372")]
        public void Maps7dReleaseIdtoMbId(int sevenDigitalId, string musicBrainzId)
        {
            var mbId = new Guid(musicBrainzId);
            var matches = _mapper.Map(sevenDigitalId);

            Assert.That(matches, Contains.Item(mbId));
        }
    }
}