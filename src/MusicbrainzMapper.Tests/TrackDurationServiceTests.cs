using System;
using NUnit.Framework;
using SevenDigital.Api.Wrapper.Exceptions;

namespace MusicbrainzMapper.Tests
{
    [TestFixture]
    public class TrackDurationServiceTests
    {
        [Test]
        public async void TrackDurationServiceGetsTrackDurationsForASevenDigitalRelease()
        {
            const int releaseId = 12345;
            var service = new TrackDurationService();

            var trackDurations = await service.GetAsync(releaseId);

            Assert.That(trackDurations, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidResourceException))]
        public async void InvalidReleaseIdReturnsException()
        {
            const int invalidReleaseId = 0;
            var service = new TrackDurationService();

            var trackDurations = await service.GetAsync(invalidReleaseId);
        }

        [Test]
        public async void AreYouAsync()
        {
            const int releaseId = 12345;
            var service = new TrackDurationService();

            var referenceTime = DateTime.Now;
            var taskCompletedTimeTask = service.GetAsync(releaseId).ContinueWith(task => DateTime.Now);
            var taskSpecifiedTime = DateTime.Now;

            var taskCompletedTime = await taskCompletedTimeTask;

            var taskCreationDelta = taskSpecifiedTime - referenceTime;
            var taskCompletedDelta = taskCompletedTime - taskSpecifiedTime;

            Assert.That(referenceTime, Is.LessThan(taskSpecifiedTime));
            Assert.That(referenceTime, Is.LessThan(taskCompletedTime));
            Assert.That(taskSpecifiedTime, Is.LessThan(taskCompletedTime));
            Assert.That(taskCreationDelta, Is.LessThan(taskCompletedDelta));
        }
    }
}