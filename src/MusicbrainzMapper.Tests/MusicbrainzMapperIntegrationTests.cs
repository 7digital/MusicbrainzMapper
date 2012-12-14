using System;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MusicbrainzMapperIntegrationTests
    {
        private const string ConnectionString = "Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;";

        [TestCase(287887, "e956c901-acb7-48d6-9dc6-389a5f91f372")]
        public async void Maps7dReleaseIdtoMbId(int sevenDigitalId, string musicBrainzId)
        {
            var mbId = new Guid(musicBrainzId);
            var matcher = new TrackDurationMatcher(ConnectionString);
            var durationService = new TrackDurationService();
            var mapper = new SevenDigitalToMusicBrainzMapper(durationService, matcher);

            var matches = await mapper.MapAsync(sevenDigitalId);

            Assert.That(matches, Contains.Item(mbId));
        }

    }
}