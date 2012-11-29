using System.Collections.Generic;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationMatcherTests
    {
        [Test]
        public async void TrackDurationMatcherFetchesNonNullForNonNullInput()
        {
            var matcher = new TrackDurationMatcher();
            var matches = await matcher.FindMatchesAsync(new List<int>());

            Assert.That(matches, Is.Not.Null);
        }
    }
}