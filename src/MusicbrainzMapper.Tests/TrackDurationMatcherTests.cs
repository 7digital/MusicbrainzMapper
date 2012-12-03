using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationMatcherTests: AsyncSanityTest
    {
        private TrackDurationMatcher _matcher;

        [SetUp]
        public void Setup()
        {
            _matcher = new TrackDurationMatcher();
        }

        [Test]
        public async void TrackDurationMatcherFetchesNonNullForNonNullInput()
        {
            var matches = await GetMatches();

            Assert.That(matches, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public async void EmptyTrackDurationListThrowsException()
        {
            var matches = await _matcher.FindMatchesAsync(new int[0]);
        }

        [TearDown]
        public void Dispose()
        {
            _matcher.Dispose();
        }

        protected async override Task ToTest()
        {
            await GetMatches();
        }

        private async Task<IList<Guid>> GetMatches()
        {
            return await _matcher.FindMatchesAsync(new[] { 1, 2, 3 });
        }

    }
}