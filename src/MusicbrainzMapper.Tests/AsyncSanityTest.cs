using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    public abstract class AsyncSanityTest
    {
        [Test]
        public async void AreYouAsync()
        {
            var referenceTime = DateTime.Now;
            var taskCompletedTimeTask = ToTest().ContinueWith(task => DateTime.Now);
            var taskSpecifiedTime = DateTime.Now;

            var taskCompletedTime = await taskCompletedTimeTask;

            var taskCreationDelta = taskSpecifiedTime - referenceTime;
            var taskCompletedDelta = taskCompletedTime - taskSpecifiedTime;

            Assert.That(referenceTime, Is.LessThan(taskSpecifiedTime));
            Assert.That(referenceTime, Is.LessThan(taskCompletedTime));
            Assert.That(taskSpecifiedTime, Is.LessThan(taskCompletedTime));
            Assert.That(taskCreationDelta, Is.LessThan(taskCompletedDelta));
        }

        protected abstract Task ToTest();
    }
}