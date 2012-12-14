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

        [SetUp]
        public void Setup()
        {
            _matcher = new MusicbrainzTrackDurationMatcher();
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