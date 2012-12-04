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
        private readonly int[] _validTrackDurations = new[] { 1, 2, 3 };
        private const string ConnectionString = "Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;";

        [SetUp]
        public void Setup()
        {
            _matcher = new TrackDurationMatcher(ConnectionString);
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
            await _matcher.FindMatchesAsync(new int[0]);
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
            return await _matcher.FindMatchesAsync(_validTrackDurations);
        }

    }
}