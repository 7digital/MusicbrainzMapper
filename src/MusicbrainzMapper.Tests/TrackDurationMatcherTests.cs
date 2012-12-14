using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationMatcherTests
    {
        private MusicbrainzTrackDurationMatcher _matcher;
        private readonly int[] _validTrackDurations = new[] { 1, 2, 3 };
        private const string ConnectionString = "Server=10.0.10.119;Port=5432;User Id=musicbrainz;Password=musicbrainz;Database=musicbrainz_db;";

        [SetUp]
        public void Setup()
        {
            _matcher = new MusicbrainzTrackDurationMatcher(ConnectionString);
        }

        [Test]
        public void TrackDurationMatcherFetchesNonNullForNonNullInput()
        {
            var matches = GetMatches();

            Assert.That(matches, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void EmptyTrackDurationListThrowsException()
        {
            _matcher.FindMatches(new int[0]);
        }

        [TearDown]
        public void Dispose()
        {
            _matcher.Dispose();
        }

        private IList<Guid> GetMatches()
        {
            return _matcher.FindMatches(_validTrackDurations);
        }

    }
}