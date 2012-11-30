using System;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class MusicbrainzMapperIntegrationTests
    {
        private const int RageAgainstTheMachine7dId = 287887;
        private readonly Guid RageAgainstTheMachineMbId = new Guid("e956c901-acb7-48d6-9dc6-389a5f91f372");

        [Test]
        public async void Maps7dReleaseIdtoMbId()
        {
            var matcher = new TrackDurationMatcher();
            var durationService = new TrackDurationService();
            var mapper = new SevenDigitalToMusicBrainzMapper(durationService, matcher);

            var matches = await mapper.MapAsync(RageAgainstTheMachine7dId);

            Assert.That(matches, Contains.Item(RageAgainstTheMachineMbId));
        }
    }
}