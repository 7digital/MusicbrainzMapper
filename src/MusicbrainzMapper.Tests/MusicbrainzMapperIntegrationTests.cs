﻿using System;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MusicbrainzMapperIntegrationTests
    {
        private SevenDigitalToMusicBrainzMapper _mapper;
        private MusicbrainzTrackDurationMatcher _trackDurationMatcher;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var durationService = new SevenDigitalTrackDurationService();
            _trackDurationMatcher = new MusicbrainzTrackDurationMatcher();
            _mapper = new SevenDigitalToMusicBrainzMapper(durationService, _trackDurationMatcher);
        }

        [TestCase(287887, "e956c901-acb7-48d6-9dc6-389a5f91f372", "Rage Against The Machine")]
        [TestCase(1076864, "c51eb64c-e4e2-3992-8f4e-49909853a663", "21")]
        [TestCase(499472, "3fed3ec7-96da-497e-989f-0b9e711378d2", "Emigrantski Raggamuffin")]
        [TestCase(28815, "497f9acf-a695-332e-bc20-1c8745248550", "Homework")]
        public void Maps7dReleaseIdtoMbId(int sevenDigitalId, string musicBrainzId, string releaseName)
        {
            var mbId = new Guid(musicBrainzId);
            var matches = _mapper.Map(sevenDigitalId);

            Assert.That(matches, Contains.Item(mbId), string.Format("Could not map {0}", releaseName));
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _trackDurationMatcher.Dispose();
        }
    }
}